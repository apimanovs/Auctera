namespace Auctera.Auctions.Application.Models;

public sealed class CurrentPriceDto
{
    public Guid AuctionId { get; init; }
    public decimal Price { get; init; }
    public string Currency { get; init; } = default!;
}
