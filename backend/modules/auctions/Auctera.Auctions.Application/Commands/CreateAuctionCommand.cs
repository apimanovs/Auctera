using MediatR;

namespace Auctera.Auctions.Application.Commands;

/// <summary>
/// Represents the create auction command record.
/// </summary>
public sealed record CreateAuctionCommand (Guid LotId) : IRequest<Guid>;
