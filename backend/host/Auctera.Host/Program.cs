using System.Threading.RateLimiting;
using Auctera.Auctions.API.Controllers;
using Auctera.Auctions.Application.Interfaces;
using Auctera.Auctions.Infrastructure.Repository;
using Auctera.Bids.API.Controllers;
using Auctera.Host.Middleware;
using Auctera.Host.BackgroundJobs;
using Auctera.Identity.API.Controllers;
using Auctera.Identity.Infrastructure;
using Auctera.Items.API.Controllers;
using Auctera.Items.Application.Interfaces;
using Auctera.Items.Infrastructure.Repository;
using Auctera.Persistance;
using Auctera.Realtime.Extensions;
using Auctera.Shared.Domain.Time;
using Auctera.Shared.Infrastructure.Dispatcher;
using Auctera.Shared.Infrastructure.Interfaces;
using Auctera.Shared.Infrastructure.Time;
using Auctera.Shared.Infrastructure.Media;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(AuctionsController).Assembly)
    .AddApplicationPart(typeof(EngineController).Assembly)
    .AddApplicationPart(typeof(ItemController).Assembly)
    .AddApplicationPart(typeof(BidsController).Assembly)
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

    options.AddPolicy("AuthPolicy", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 10,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0,
                AutoReplenishment = true
            }));

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

builder.Services.AddDbContext<AucteraDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
});

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Auctera.Items.Application.Marker.MediatRAssemblyMarker).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(Auctera.Bids.Application.Marker.MediatRAssemblyMarker).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(Auctera.Auctions.Application.Marker.MediatRAssemblyMarker).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(Auctera.Identity.Application.Handlers.LoginCommandHandler).Assembly);
});

builder.Services.AddScoped<IAuctionRepository, AuctionRepository>();
builder.Services.AddScoped<ILotRepository, LotRepository>();
builder.Services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
builder.Services.AddScoped<IClock, SystemClock>();
builder.Services.AddTransient<GlobalExceptionMiddleware>();
builder.Services.AddHostedService<AuctionAutoStopBackgroundService>();

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
