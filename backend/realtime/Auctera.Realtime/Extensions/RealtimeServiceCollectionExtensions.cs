using Auctera.Auctions.Application.Interfaces;
using Auctera.Realtime.Notifier;
using Microsoft.Extensions.DependencyInjection;

namespace Auctera.Realtime.Extensions;

/// <summary>
/// Represents the realtime service collection extensions class.
/// </summary>
public static class RealtimeServiceCollectionExtensions
{
    /// <summary>
    /// Adds auctera realtime.
    /// </summary>
    /// <param name="services">Services.</param>
    /// <returns>The operation result.</returns>
    public static IServiceCollection AddAucteraRealtime(this IServiceCollection services)
    {
        services.AddSignalR();

        services.AddScoped<
            IAuctionRealtimeNotifier,
            SignalRAuctionRealtimeNotifier>();

        return services;
    }
}
