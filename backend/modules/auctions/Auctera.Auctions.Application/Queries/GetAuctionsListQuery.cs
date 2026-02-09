using Auctera.Auctions.Application.Models;
using Auctera.Shared.Domain.Enums;

using MediatR;

namespace Auctera.Auctions.Application.Queries;

public record GetAuctionsListQuery() : IRequest<IReadOnlyList<AuctionListItemDto>>;
