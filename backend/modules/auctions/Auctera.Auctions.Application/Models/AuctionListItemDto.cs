using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctera.Auctions.Application.Models;

public sealed class AuctionListItemDto
{
    public Guid AuctionId { get; init; }
    public Guid LotId { get; init; } = default!;
    public string Status { get; init; } = default!;
    public DateTime? EndDate { get; init; }
    public decimal? CurrentPrice { get; init; }
    public string? Currency { get; init; } = default!;
}
