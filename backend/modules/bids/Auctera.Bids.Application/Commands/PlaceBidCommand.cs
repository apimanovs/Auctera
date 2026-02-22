using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Auctera.Bids.Application.Commands;

public sealed record PlaceBidCommand(
    Guid AuctionId,

    [property: Required]
    Guid BidderId,

    [property: Range(typeof(decimal), "0.01", "1000000000")]
    decimal Amount,

    [property: Required]
    [property: RegularExpression("^(USD|EUR|GBP|JPY|AUD|CAD|CHF|CNY|SEK|NZD)$")]
    string Currency
) : IRequest;
