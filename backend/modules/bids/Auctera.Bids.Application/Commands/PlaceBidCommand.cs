using MediatR;

namespace Auctera.Bids.Application.Commands;

public sealed record PlaceBidCommand(
    Guid AuctionId,
    Guid BidderId,
    decimal Amount,
    string Currency
) : IRequest;
