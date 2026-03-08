using Auctera.Bids.Application.Models;

using MediatR;

namespace Auctera.Bids.Application.Queries;

/// <summary>
/// Represents the get bids by auction query record.
/// </summary>
public sealed record GetBidsByAuctionQuery(Guid AuctionId) : IRequest<IReadOnlyList<BidsByAuctionDto>>;
