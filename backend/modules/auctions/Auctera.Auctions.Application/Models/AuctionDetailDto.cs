using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctera.Auctions.Application.Models;
public sealed class AuctionDetailsDto
{
    public Guid AuctionId { get; init; }
    public string Status { get; init; } = default!;

    public decimal? CurrentPrice { get; init; }
    public string? Currency { get; init; } = default!;

    public DateTime? StartsAt { get; init; }
    public DateTime? EndsAt { get; init; }

    public Guid? LotId { get; init; }
    public string LotTitle { get; init; } = default!;
}
