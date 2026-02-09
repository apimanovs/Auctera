using Auctera.Shared.Domain.Abstractions;
using Auctera.Shared.Domain.ValueObjects;

namespace Auctera.Auctions.Domain.Events;
public sealed class BidOutbidDomainEvent : IDomainEvent
{
    public Guid AuctionId { get; }
    public Guid OutbidBidId { get; }
    public Guid OutbidUserId { get; }
    public Money Amount { get; } = default!;
    public DateTime OccurredAt { get; }

    public BidOutbidDomainEvent
    (
        Guid auctionId,
        Guid outbidBidId,
        Guid outbidUserId,
        Money amount,
        DateTime occurredAt
    )
    {
        AuctionId = auctionId;
        OutbidBidId = outbidBidId;
        OutbidUserId = outbidUserId;
        Amount = amount;
        OccurredAt = occurredAt;
    }
}
