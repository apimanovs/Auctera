using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Auctera.Items.Domain.Enums;
using Auctera.Shared.Domain.Enums;

namespace Auctera.Admin.Domain.Models;

public sealed class PendingLotPreviewDetailsDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? DescriptionPreview { get; init; }

    public Guid SellerId { get; init; }
    public string SellerUsername { get; init; } = string.Empty;
    public string? SellerEmail { get; init; }

    public string? Brand { get; init; }
    public LotCategory? Category { get; init; }
    public LotCondition? Condition { get; init; }

    public decimal StartingPrice { get; init; }
    public string Currency { get; init; }

    public LotStatus Status { get; init; }

    public string? MainImageUrl { get; init; }
}
