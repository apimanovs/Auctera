using Auctera.Auctions.Application.Interfaces;
using Auctera.Auctions.Domain.Events;

using MediatR;

namespace Auctera.Auctions.Application.Handlers;
public sealed class AuctionRealTimeHandler(IAuctionRealtimeNotifier auctionRealtimeNotifier)
    : INotificationHandler<AuctionEndedDomainEvent>, INotificationHandler<BidPlacedDomainEvent>
{
    private readonly IAuctionRealtimeNotifier _auctionRealtimeNotifier = auctionRealtimeNotifier;

    public async Task Handle(AuctionEndedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        if (domainEvent.AuctionId is null || domainEvent.WinnerId is null || domainEvent.WinningBidId is null)
        {
            return;
        }

        await _auctionRealtimeNotifier.AuctionEnded(domainEvent.AuctionId.Value, domainEvent.WinnerId.Value, domainEvent.WinningBidId.Value);
    }

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
