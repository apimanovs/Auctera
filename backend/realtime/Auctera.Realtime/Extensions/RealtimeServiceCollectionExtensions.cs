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

        return services;
    }
}
