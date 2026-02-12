using Auctera.Shared.Domain.Enums;
using Auctera.Shared.Domain.Abstractions;
using Auctera.Shared.Domain.ValueObjects;

namespace Auctera.Items.Domain;

public sealed class Lot : Entity<Guid>
{
    public Guid SellerId { get; private set; }
    public Guid? AuctionId { get; private set; }

    public string Title { get; private set; }
    public string Description { get; private set; }

    public Money Price { get; private set; }
    public LotStatus Status { get; private set; }

    private Lot() { }

    public Lot(
        Guid id,
        Guid sellerId,
        Guid auctionId,
        string title,
        string description,
        Money price
    ) : base(id)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title is required.");
        }

        if (string.IsNullOrWhiteSpace(description) )
        {
            throw new ArgumentException("Description is required.");
        }

        if (description.Length < 100)
        {
            throw new ArgumentException("Description must be at least 100 characters.");
        }

        SellerId = sellerId;
        AuctionId = auctionId;
        Title = title;
        Description = description;
        Price = price ?? throw new ArgumentNullException(nameof(price));
        Status = LotStatus.Draft;
    }

    public void AssignToAuction(Guid auctionId)
    {
        AuctionId = auctionId;
    }

    public void Publish()
    {
        if (Status != LotStatus.Draft)
        {
            throw new InvalidOperationException("Only draft lots can be published.");
        }

        Status = LotStatus.Published;
    }
}
