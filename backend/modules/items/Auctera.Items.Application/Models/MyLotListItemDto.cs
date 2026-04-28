using Auctera.Items.Domain.Enums;
using Auctera.Shared.Domain.Enums;

namespace Auctera.Items.Application.Models;

public sealed class MyLotListItemDto
{
    public Guid Id { get; init; }
    public Guid SellerId { get; init; }
    public string Title { get; init; } = default!;
    public string Description { get; init; } = default!;
    public decimal Price { get; init; }
    public string Currency { get; init; } = default!;
    public LotCategory Category { get; init; }
    public string CategoryName { get; init; } = default!;
    public LotGender Gender { get; init; }
    public string GenderName { get; init; } = default!;
    public LotSize Size { get; init; }
    public string SizeName { get; init; } = default!;
    public string Brand { get; init; } = default!;
    public LotCondition Condition { get; init; }
    public string ConditionName { get; init; } = default!;
    public string? Color { get; init; }
    public int? Year { get; init; }
    public LotStatus Status { get; init; }
    public string StatusName { get; init; } = default!;
    public Guid? AuctionId { get; init; }
    public DateTime? CreatedAt { get; init; }
    public List<LotMediaDto> Media { get; init; } = new();
}
