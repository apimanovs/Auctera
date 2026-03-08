using MediatR;
using Auctera.Auctions.Application.Models;

namespace Auctera.Auctions.Application.Queries;

/// <summary>
/// Represents the get auction details query record.
/// </summary>
public record GetAuctionDetailsQuery(Guid AuctionId) : IRequest<AuctionDetailsDto>;
