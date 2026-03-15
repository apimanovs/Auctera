using Microsoft.AspNetCore.SignalR;
using Auctera.Auctions.Application.Interfaces;
using Auctera.Realtime.Hubs;

namespace Auctera.Realtime.Notifier;

/// <summary>
/// Represents the signal r auction realtime notifier class.
/// </summary>
public sealed class SignalRAuctionRealtimeNotifier(IHubContext<AuctionHub> hubContext) : IAuctionRealtimeNotifier
{
    private readonly IHubContext<AuctionHub> _hubContext = hubContext;

    /// <summary>
    /// Performs the bid placed operation.
    /// </summary>
    /// <param name="auctionId">Identifier of auction.</param>
    /// <param name="bidderId">Identifier of bidder.</param>
    /// <param name="amount">Amount.</param>
    /// <param name="currency">Currency.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task BidPlaced(Guid auctionId, Guid bidderId, decimal amount, string currency)
    {
        return _hubContext.Clients.All.SendAsync
        (
            "BidPlaced",
            new { auctionId, bidderId, amount, currency }
        );
    }

    /// <summary>
    /// Performs the auction ended operation.
    /// </summary>
    /// <param name="auctionId">Identifier of auction.</param>
    /// <param name="winnerId">Identifier of winner.</param>
    /// <param name="winningBidId">Identifier of winning bid.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task AuctionEnded(Guid auctionId, Guid? winnerId, Guid winningBidId)
    {
        return _hubContext.Clients.All.SendAsync
        (
            "AuctionEnded",
            new { auctionId, winnerId, winningBidId }
        );
    }

    /// <summary>
    /// Performs the auction started operation.
    /// </summary>
    /// <param name="auctionId">Identifier of auction.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task AuctionStarted(Guid auctionId)
    {
        return _hubContext.Clients.All.SendAsync
        (
            "AuctionStarted",
            new { auctionId }
        );
    }
}
