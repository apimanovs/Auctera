using MediatR;
using Auctera.Auctions.Application.Models;

namespace Auctera.Auctions.Application.Queries;
public sealed record GetCurrentPriceQuery(Guid AuctionId) : IRequest<CurrentPriceDto>;
