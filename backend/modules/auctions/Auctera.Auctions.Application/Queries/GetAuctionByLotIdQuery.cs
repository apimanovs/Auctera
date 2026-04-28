using Auctera.Auctions.Application.Models;

using MediatR;

namespace Auctera.Auctions.Application.Queries;

public sealed record GetAuctionByLotIdQuery(Guid LotId) : IRequest<AuctionDetailsDto?>;
