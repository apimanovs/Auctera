using Microsoft.AspNetCore.SignalR;
using Auctera.Auctions.Application.Interfaces;
using Auctera.Realtime.Hubs;

namespace Auctera.Realtime.Notifier;

public sealed class SignalRAuctionRealtimeNotifier(IHubContext<AuctionHub> hubContext) : IAuctionRealtimeNotifier
{
    private readonly IHubContext<AuctionHub> _hubContext = hubContext;

    public Task BidPlaced(Guid auctionId, Guid bidderId, decimal amount, string currency)
    {
        return _hubContext.Clients.All.SendAsync
        (
            "BidPlaced",
            new { auctionId, bidderId, amount, currency }
        );
    }

    public Task AuctionEnded(Guid auctionId, Guid winnerId, Guid winningBidId)
    {
        return _hubContext.Clients.All.SendAsync
        (
            "AuctionEnded",
            new { auctionId, winnerId, winningBidId }
        );
    }

    public Task AuctionStarted(Guid auctionId)
    {
        return _hubContext.Clients.All.SendAsync
        (
            "AuctionStarted",
            new { auctionId }
        );
    }
}
