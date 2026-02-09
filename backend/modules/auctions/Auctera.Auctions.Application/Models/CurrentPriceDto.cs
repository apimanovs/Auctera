using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Auctera.Shared.Domain.ValueObjects;

namespace Auctera.Auctions.Application.Models;
public sealed class CurrentPriceDto
{
    public Guid AuctionId { get; init; }
    public decimal Price { get; init; }
    public string Currency { get; init; } = default!;
}
