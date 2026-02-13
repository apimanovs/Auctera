using Auctera.Shared.Domain.Abstractions;

using MediatR;

namespace Auctera.Auctions.Domain.Events;

public sealed class AuctionEndedDomainEvent : IDomainEvent
{
    public Guid AuctionId { get; }
    public Guid WinnerId { get; }
    public Guid WinningBidId { get; }
    public DateTime OccurredAt { get; }

    public AuctionEndedDomainEvent(Guid auctionId, Guid winnerId, Guid winningBidId, DateTime occurredAt)
    {
        AuctionId = auctionId;
        WinnerId = winnerId;
        WinningBidId = winningBidId;
        OccurredAt = occurredAt;
    }
}
