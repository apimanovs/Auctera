using Auctera.Auctions.Application.Interfaces;
using Auctera.Realtime.Notifier;
using Microsoft.Extensions.DependencyInjection;

namespace Auctera.Realtime.Extensions;

public static class RealtimeServiceCollectionExtensions
{
    public static IServiceCollection AddAucteraRealtime(this IServiceCollection services)
    {
        services.AddSignalR();

        services.AddScoped<
            IAuctionRealtimeNotifier,
            SignalRAuctionRealtimeNotifier>();

        services.AddCors(options =>
        {
            options.AddPolicy("RealtimeCors", policy =>
            {
                policy
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .SetIsOriginAllowed(_ => true);
            });
        });

        return services;
    }
}
