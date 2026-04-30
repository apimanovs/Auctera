using System.Threading.RateLimiting;

using Amazon.Runtime;
using Amazon.S3;

using Auctera.Admin.API;
using Auctera.Admin.Application.Queries;
using Auctera.Auctions.API.Controllers;
using Auctera.Auctions.Application.Interfaces;
using Auctera.Auctions.Infrastructure.Repository;
using Auctera.Bids.API.Controllers;
using Auctera.Bids.Application.Interfaces;
using Auctera.Bids.Infrastructure.Repository;
using Auctera.Host.BackgroundJobs;
using Auctera.Host.Middleware;
using Auctera.Identity.API.Controllers;
using Auctera.Identity.Application.Interfaces;
using Auctera.Identity.Infrastructure;
using Auctera.Identity.Infrastructure.Cookies;
using Auctera.Identity.Infrastructure.Repository;
using Auctera.Items.API.Controllers;
using Auctera.Items.Application.Interfaces;
using Auctera.Items.Infrastructure.Repository;
using Auctera.Orders.API.Controllers;
using Auctera.Orders.Application.Interfaces;
using Auctera.Orders.Infrastructure.Options;
using Auctera.Orders.Infrastructure.Repository;
using Auctera.Persistance;
using Auctera.Realtime.Extensions;
using Auctera.Shared.Domain.Time;
using Auctera.Shared.Infrastructure.Dispatcher;
using Auctera.Shared.Infrastructure.Interfaces;
using Auctera.Shared.Infrastructure.Media;
using Auctera.Shared.Infrastructure.Time;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MediaOptions>(
    builder.Configuration.GetSection(MediaOptions.SectionName));

var mediaOptions = builder.Configuration
    .GetSection(MediaOptions.SectionName)
    .Get<MediaOptions>() ?? throw new InvalidOperationException("R2 configuration is missing.");

builder.Services.AddSingleton<IAmazonS3>(_ =>
{
    var credentials = new BasicAWSCredentials(
        mediaOptions.AccessKeyId,
        mediaOptions.SecretAccessKey);

    var config = new AmazonS3Config
    {
        ServiceURL = mediaOptions.ServiceUrl,
        ForcePathStyle = true
    };

    return new AmazonS3Client(credentials, config);
});

builder.Services.AddControllers()
    .AddApplicationPart(typeof(AuctionsController).Assembly)
    .AddApplicationPart(typeof(EngineController).Assembly)
    .AddApplicationPart(typeof(ItemController).Assembly)
    .AddApplicationPart(typeof(BidsController).Assembly)
    .AddApplicationPart(typeof(AdminController).Assembly)
    .AddApplicationPart(typeof(OrdersController).Assembly)
    .AddApplicationPart(typeof(AuthController).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();

builder.Services.AddAucteraRealtime();
builder.Services.AddIdentityModule(builder.Configuration);
builder.Services.AddMedia(builder.Configuration);

var corsOrigins = builder.Environment.IsDevelopment()
    ? builder.Configuration.GetSection("Cors:DevOrigins").Get<string[]>()
    : builder.Configuration.GetSection("Cors:ProdOrigins").Get<string[]>();

if (corsOrigins is null || corsOrigins.Length == 0)
{
    throw new InvalidOperationException($"No CORS origins configured for environment '{builder.Environment.EnvironmentName}'.");
}

builder.Services.AddCors(options =>
{
    options.AddPolicy("RealtimeCors", policy =>
    {
        policy
            .WithOrigins(corsOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddPolicy("AuthLoginPolicy", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0,
                AutoReplenishment = true
            }));

    options.AddPolicy("AuthRefreshPolicy", httpContext =>
    {
        var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        return RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: $"refresh:{ip}",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 30,
                Window = TimeSpan.FromSeconds(30),
                QueueLimit = 0,
                AutoReplenishment = true
            });
    });

    options.AddPolicy("AuthMePolicy", httpContext =>
    {
        var userId = httpContext.User.FindFirst("sub")?.Value
            ?? httpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value
            ?? httpContext.Connection.RemoteIpAddress?.ToString()
            ?? "unknown";

        return RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: $"me:{userId}",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 60,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0,
                AutoReplenishment = true
            });
    });

    options.AddPolicy("BidsPolicy", httpContext =>
    {
        var userId = httpContext.User.FindFirst("sub")?.Value
            ?? httpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var partition = userId is null ? $"ip:{ip}" : $"user:{userId}";

        return RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: partition,
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 6,
                Window = TimeSpan.FromSeconds(10),
                QueueLimit = 0,
                AutoReplenishment = true
            });
    });
});

builder.Services
    .AddOptions<StripeOptions>()
    .Bind(builder.Configuration.GetSection(StripeOptions.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddSingleton<Stripe.Checkout.SessionService>();

var stripeOptions = builder.Configuration.GetSection(StripeOptions.SectionName).Get<StripeOptions>()!;
Stripe.StripeConfiguration.ApiKey = stripeOptions.SecretKey;

builder.Services.AddDbContext<AucteraDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
});

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Auctera.Items.Application.Marker.MediatRAssemblyMarker).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(Auctera.Bids.Application.Marker.MediatRAssemblyMarker).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(Auctera.Auctions.Application.Marker.MediatRAssemblyMarker).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(Auctera.Orders.Application.Marker.MediatRAssemblyMarker).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(Auctera.Identity.Application.Handlers.LoginCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetAdminDashboardData).Assembly);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins(corsOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddAuthorization();
builder.Services.AddScoped<IAuctionRepository, AuctionRepository>();
builder.Services.AddScoped<IBidRepository, BidRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<ILotRepository, LotRepository>();
builder.Services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
builder.Services.AddScoped<ICookieFactory, CookieFactory>();
builder.Services.AddScoped<IOrderRepository, OrdersRepository>();

builder.Services.AddSingleton<IClock, SystemClock>();

builder.Services.AddHostedService<AuctionAutoStopBackgroundService>();

builder.Services.AddScoped<IMediaUploader, MediaUploader>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseCors("RealtimeCors");
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();

app.MapAucteraRealtime();
app.MapControllers();

app.Run();
