using Auctera.Shared.Domain.Abstractions;
using Auctera.Shared.Domain.ValueObjects;

namespace Auctera.Auctions.Domain.Events;
public sealed class BidPlacedDomainEvent : IDomainEvent
{
    public Guid AuctionId { get; }
    public Guid BidId { get; }
    public Guid UserId { get; }
    public Money Money { get; } = default!;
    public DateTime OccurredAt { get; }

    public BidPlacedDomainEvent
    (
        Guid auctionId,
        Guid bidId,
        Guid userId,
        Money money,
        DateTime occeredAt
    )
    {
        AuctionId = auctionId;
        BidId = bidId;
        UserId = userId;
        Money = money;
        OccurredAt = occeredAt;
    }
}
