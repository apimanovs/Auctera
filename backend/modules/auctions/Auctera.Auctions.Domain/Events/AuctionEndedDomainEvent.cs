using Auctera.Shared.Domain.Abstractions;
using Auctera.Shared.Domain.ValueObjects;

using MediatR;

namespace Auctera.Auctions.Domain.Events;

public sealed class AuctionEndedDomainEvent : IDomainEvent
{
    public Guid AuctionId { get; }
    public Guid LotId { get; }
    public Guid WinnerId { get; }
    public Guid? WinningBidId { get; }
    public Money? WinningMoney { get; }
    public DateTime OccurredAt { get; }

    public AuctionEndedDomainEvent(
        Guid auctionId,
        Guid winnerId,
        Guid winningBidId,
        Money? winningMoney,
        DateTime occurredAt)
    {
        AuctionId = auctionId;
        WinnerId = winnerId;
        WinningBidId = winningBidId;
        WinningMoney = winningMoney;
        OccurredAt = occurredAt;
    }
}
