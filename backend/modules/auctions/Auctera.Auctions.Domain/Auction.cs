using Auctera.Shared.Domain.Abstractions;
using Auctera.Shared.Domain.Time;
using Auctera.Shared.Domain.ValueObjects;
using Auctera.Shared.Domain.Enums;
using Auctera.Bids.Domain;
using Auctera.Items.Domain;
using Auctera.Auctions.Domain.Events;

namespace Auctera.Auctions.Domain;
public sealed class Auction : AggregateRoot<Guid>
{
    public AuctionStatus Status { get; private set; }
    public DateTime? StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public Money? CurrentPrice { get; private set; }
    public Guid LotId { get; private set; }

    private readonly List<Lot> _lots = new();
    public IReadOnlyCollection<Lot> Lots => _lots;

    private readonly List<Bid> _bids = new();
    public IReadOnlyCollection<Bid> Bids => _bids;

    private static readonly TimeSpan AntiSnipingWindow = TimeSpan.FromSeconds(10);
    private static readonly TimeSpan AntiSnipingExtension = TimeSpan.FromSeconds(30);

    public Auction(Guid id) : base(id)
    {
        Status = AuctionStatus.Draft;
    }

    public void StartAuction(DateTime now, TimeSpan duration)
    {
        if (Status != AuctionStatus.Draft)
        {
            throw new InvalidOperationException("Auction already started.");
        }

        if (duration <= TimeSpan.Zero)
        {
            throw new ArgumentException("Duration must be positive.");
        }

        if (duration > TimeSpan.FromDays(30))
        {
            throw new ArgumentException("Auction must be no longer than 30 days.");
        }

        StartDate = now;
        EndDate = now.Add(duration);
        Status = AuctionStatus.Active;
    }

    public void StopAuction(DateTime now)
    {
        if (Status != AuctionStatus.Active)
        {
            throw new InvalidOperationException("Only active auctions can be stopped.");
        }

        if (EndDate == null || now < EndDate.Value)
        {
            throw new InvalidOperationException("Auction has not ended yet.");
        }

        Bid? winningBid = null;

        if (_bids.Any())
        {
            winningBid = _bids
                .OrderByDescending(b => b.Amount.Amount)
                .First();

            foreach (var bid in _bids)
            {
                if (bid == winningBid)
                {
                    bid.MarkAsWinning();
                }
                else
                {
                    bid.MarkAsOutbid();
                }
            }
        }

        Status = AuctionStatus.Finished;

        AddDomainEvent(new AuctionEndedDomainEvent(
            auctionId: Id,
            winnerId: winningBid.BidderId,
            winningBidId: winningBid.Id,
            occurredAt: now
        ));
    }

    public void CancelAuction()
    {
        if (Status == AuctionStatus.Finished)
        {
            throw new InvalidOperationException("Finished auctions cannot be cancelled.");
        }

        Status = AuctionStatus.Cancelled;
    }

    public void RescheduleAuction(DateTime newStartDate, TimeSpan newDuration)
    {
        if (Status != AuctionStatus.Draft)
        {
            throw new InvalidOperationException("Only draft auctions can be rescheduled.");
        }

        if (newDuration <= TimeSpan.Zero)
        {
            throw new ArgumentException("Duration must be positive.");
        }

        if (newDuration > TimeSpan.FromDays(30))
        {
            throw new ArgumentException("Auction must be no longer than 30 days.");
        }

        StartDate = newStartDate;
        EndDate = newStartDate.Add(newDuration);
    }

    public void PlaceBid(
    Guid bidId,
    Guid userId,
    Money amount,
    IClock clock
    )
    {
        if (EndDate == null || clock.UtcNow >= EndDate)
        {
            throw new InvalidOperationException("Cannot place bid on ended auction.");
        }

        if (CurrentPrice != null && !amount.GreatherThan(CurrentPrice))
        { 
            throw new InvalidOperationException("Bid must be higher than current price.");
        }

        var lastBid = _bids
            .Where(b => b.Status == BidStatus.Active)
            .OrderByDescending(b => b.PlacedAt)
            .FirstOrDefault();

        if (lastBid != null)
        {
            lastBid.MarkAsOutbid();
        }

        var bid = new Bid(
            bidId,
            userId, 
            Id,
            amount,
            clock.UtcNow
        );

        _bids.Add(bid);
        CurrentPrice = amount;

        AntiSnipping(clock.UtcNow);

        AddDomainEvent(new BidPlacedDomainEvent(Id, bidId, userId, amount, clock.UtcNow));
    }

    public void AddLot(Lot lot)
    {
        if (lot == null)
        {
            throw new ArgumentNullException("Lot cannot be null.");
        }

        if (Status != AuctionStatus.Draft)
        {
            throw new InvalidOperationException("Cannot add lots to a started auction.");
        }

        if (lot.Status != LotStatus.Published)
        {
            throw new InvalidOperationException("Only published lots can be added to auction.");
        }

        if (Lots.Contains(lot))
        {
            throw new InvalidOperationException("Lot is already added to this auction.");
        }

        if (_lots.Count >= 1)
        {
            throw new InvalidOperationException("Auction can have only one lot.");
        }

        _lots.Add(lot);
    }

    private void RemoveLot(Lot lot)
    {
        if (lot == null)
        {
            throw new ArgumentNullException("Lot cannot be null.");
        }
        if (Status != AuctionStatus.Draft)
        {
            throw new InvalidOperationException("Cannot remove lots from a started auction.");
        }
        if (!Lots.Contains(lot))
        {
            throw new InvalidOperationException("Lot is not part of this auction.");
        }
        _lots.Remove(lot);
    }

    private void AddBid(Bid bid)
    {
        if (bid == null)
        {
            throw new ArgumentNullException("Bid cannot be null.");
        }

        if (Status != AuctionStatus.Active)
        {
            throw new InvalidOperationException("Bids can only be added to active auctions.");
        }

        _bids.Add(bid);
    }

    private void AntiSnipping(DateTime now)
    {
        if (EndDate == null)
        {
            throw new InvalidOperationException("Auction end date is not set.");
        }

        var timeRemaining = EndDate.Value - now;

        if (timeRemaining <= AntiSnipingWindow)
        {
            EndDate = EndDate.Value.Add(AntiSnipingExtension);
        }
    }
}

