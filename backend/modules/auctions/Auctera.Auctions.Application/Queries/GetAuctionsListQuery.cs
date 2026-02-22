using Auctera.Auctions.Application.Models;

using MediatR;

namespace Auctera.Auctions.Application.Queries;

public sealed record GetAuctionsListQuery : IRequest<IReadOnlyList<AuctionListItemDto>>
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 20;
    public string? Status { get; init; }
    public string? Sort { get; init; }
}
