using Auctera.Bids.Application.Models;

using MediatR;

namespace Auctera.Bids.Application.Queries;

public sealed record GetBidsByAuctionQuery(Guid AuctionId) : IRequest<IReadOnlyList<BidsByAuctionDto>>;
