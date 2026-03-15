using Auctera.Items.Domain.Enums;

using static Auctera.Items.Domain.Lot;

namespace Auctera.Items.Application.Models;

/// <summary>
/// Represents the lot dto class.
/// </summary>
public sealed class LotDto
{
    /// <summary>
    /// Gets or sets the id used by this type.
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Gets or sets the seller id used by this type.
    /// </summary>
    public Guid SellerId { get; set; }

    /// <summary>
    /// Gets or sets the title used by this type.
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// Gets or sets the description used by this type.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the price used by this type.
    /// </summary>
    public decimal Price { get; set; }
    /// <summary>
    /// Gets or sets the currency used by this type.
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Gets or sets the category used by this type.
    /// </summary>
    public LotCategory Category { get; set; }
    /// <summary>
    /// Gets or sets the gender used by this type.
    /// </summary>
    public LotGender Gender { get; set; }
    /// <summary>
    /// Gets or sets the size used by this type.
    /// </summary>
    public LotSize Size { get; set; }
    /// <summary>
    /// Gets or sets the brand used by this type.
    /// </summary>
    public string Brand { get; set; }
    /// <summary>
    /// Gets or sets the condition used by this type.
    /// </summary>
    public LotCondition Condition { get; set; }
    /// <summary>
    /// Gets or sets the color used by this type.
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// Gets or sets the media used by this type.
    /// </summary>
    public List<LotMediaDto> Media { get; set; } = new();
}
