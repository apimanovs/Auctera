using MediatR;

namespace Auctera.Auctions.Application.Commands;

/// <summary>
/// Represents the stop auction command record.
/// </summary>
public record StopAuctionCommand(Guid AuctionId) : IRequest;
