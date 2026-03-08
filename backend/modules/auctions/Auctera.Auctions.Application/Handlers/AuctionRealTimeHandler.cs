using Auctera.Auctions.Application.Interfaces;
using Auctera.Auctions.Domain.Events;

using MediatR;

namespace Auctera.Auctions.Application.Handlers;
/// <summary>
/// Represents the auction real time handler class.
/// </summary>
public sealed class AuctionRealTimeHandler(IAuctionRealtimeNotifier auctionRealtimeNotifier)
    : INotificationHandler<AuctionEndedDomainEvent>, INotificationHandler<BidPlacedDomainEvent>
{
    private readonly IAuctionRealtimeNotifier _auctionRealtimeNotifier = auctionRealtimeNotifier;

    /// <summary>
    /// Handles the operation.
    /// </summary>
    /// <param name="domainEvent">Domain event.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Handle(AuctionEndedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        if (domainEvent.AuctionId == null  || domainEvent.WinnerId == null || domainEvent.WinningBidId == null)
        {
            return;
        }

        await _auctionRealtimeNotifier.AuctionEnded(domainEvent.AuctionId, domainEvent.WinnerId, domainEvent.WinningBidId.Value);
    }

    /// <summary>
    /// Handles the operation.
    /// </summary>
    /// <param name="domainEvent">Domain event.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Handle(BidPlacedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        await _auctionRealtimeNotifier.BidPlaced(
            domainEvent.AuctionId,
            domainEvent.UserId,
            domainEvent.Money.Amount,
            domainEvent.Money.Currency
        );
    }
}
