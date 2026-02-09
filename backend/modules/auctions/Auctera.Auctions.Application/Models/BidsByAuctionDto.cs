using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctera.Auctions.Application.Models;
public sealed class BidsByAuctionDto
{
    public Guid BidId { get; init; }
    public Guid UserId { get; init; }
    public decimal Amount { get; init; }
    public string Currency { get; init; } = default!;
    public string Status { get; init; } = default!;
    public DateTime PlacedAt { get; init; }
}
