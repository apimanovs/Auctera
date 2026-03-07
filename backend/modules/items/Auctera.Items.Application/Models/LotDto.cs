using Auctera.Items.Domain.Enums;

using static Auctera.Items.Domain.Lot;

namespace Auctera.Items.Application.Models;

public sealed class LotDto
{
    public Guid Id { get; set; }
    public Guid SellerId { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }

    public decimal Price { get; set; }
    public string Currency { get; set; }

    public LotCategory Category { get; set; }
    public LotGender Gender { get; set; }
    public LotSize Size { get; set; }
    public string Brand { get; set; }
    public LotCondition Condition { get; set; }
    public string? Color { get; set; }

    public List<LotMediaDto> Media { get; set; } = new();
}
