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
app.UseAuthentication();
app.UseAuthorization();

app.MapAucteraRealtime();
app.MapControllers();

app.Run();
