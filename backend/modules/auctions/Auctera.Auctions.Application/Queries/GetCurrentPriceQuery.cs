using MediatR;
using Auctera.Auctions.Application.Models;

namespace Auctera.Auctions.Application.Queries;
/// <summary>
/// Represents the get current price query record.
/// </summary>
public sealed record GetCurrentPriceQuery(Guid AuctionId) : IRequest<CurrentPriceDto>;
