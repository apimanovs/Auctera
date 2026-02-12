using MediatR;

namespace Auctera.Auctions.Application.Commands;

public sealed record CreateAuctionCommand (Guid LotId, DateTime EndsAt) : IRequest<Guid>;
