using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Auctera.Realtime.Hubs;

namespace Auctera.Realtime.Extensions;

/// <summary>
/// Represents the realtime endpoint extensions class.
/// </summary>
public static class RealtimeEndpointExtensions
{
    public static IEndpointRouteBuilder MapAucteraRealtime(
        this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHub<AuctionHub>("/hubs/auction");
        return endpoints;
    }
}
