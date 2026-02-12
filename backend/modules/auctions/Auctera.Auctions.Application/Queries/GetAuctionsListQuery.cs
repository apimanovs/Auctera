using Auctera.Auctions.Application.Models;

using MediatR;

namespace Auctera.Auctions.Application.Queries;

public record GetAuctionsListQuery() : IRequest<IReadOnlyList<AuctionListItemDto>>;
