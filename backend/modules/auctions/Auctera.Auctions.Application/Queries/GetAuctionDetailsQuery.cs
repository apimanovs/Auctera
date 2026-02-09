using MediatR;
using Auctera.Auctions.Application.Models;

namespace Auctera.Auctions.Application.Queries;

public record GetAuctionDetailsQuery(Guid AuctionId) : IRequest<AuctionDetailsDto>;
