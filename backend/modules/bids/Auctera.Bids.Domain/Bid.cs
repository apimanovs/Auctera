using Auctera.Shared.Domain.Abstractions;
using Auctera.Shared.Domain.Enums;
using Auctera.Shared.Domain.ValueObjects;

namespace Auctera.Bids.Domain;

public class Bid : Entity<Guid>
{
    public Guid Id { get; set; }
    public Guid BidderId { get; private set; }
    public Guid AuctionId { get; private set; }
    public Money Amount { get; private set; }
    public DateTime PlacedAt { get; private set; }
    public BidStatus Status { get; private set; }

    private Bid() { }

    public Bid(
        Guid id,
        Guid bidderId,
        Guid auctionId,
        Money amount,
        DateTime placedAt
        ) : base(id)
    {
        Id = id;
        BidderId = bidderId;
        AuctionId = auctionId;
        Amount = amount;
        PlacedAt = placedAt;
    }

    public void MarkAsWinning()
    {
        Status = BidStatus.Winning;
    }

    public void MarkAsOutbid()
    {
        Status = BidStatus.Outbid;
    }
}

