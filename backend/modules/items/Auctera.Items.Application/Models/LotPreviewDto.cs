using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Auctera.Items.Domain.Enums;
using Auctera.Shared.Domain.Enums;

namespace Auctera.Items.Application.Models;

public class LotPreviewDto
{
    /// <summary>
    /// Gets or sets the id used by this type.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the title used by this type.
    /// </summary>
    public string Title { get; set; }

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
    public string CategoryName { get; set; }
    /// <summary>
    /// Gets or sets the gender used by this type.
    /// </summary>
    public LotGender Gender { get; set; }
    public string GenderName { get; set; }
    /// <summary>
    /// Gets or sets the size used by this type.
    /// </summary>
    public LotSize Size { get; set; }
    public string SizeName { get; set; }
    /// <summary>
    /// Gets or sets the brand used by this type.
    /// </summary>
    public string Brand { get; set; }
    /// <summary>
    /// Gets or sets the condition used by this type.
    /// </summary>
    public LotCondition Condition { get; set; }
    public string ConditionName { get; set; }
    /// <summary>
    /// Gets or sets the color used by this type.
    /// </summary>
    public string? Color { get; set; }

    public LotStatus Status { get; set; }
    public string StatusName { get; set; }
    /// <summary>
    /// Gets or sets the media used by this type.
    /// </summary>
    public List<LotMediaDto> Media { get; set; } = new();
}
