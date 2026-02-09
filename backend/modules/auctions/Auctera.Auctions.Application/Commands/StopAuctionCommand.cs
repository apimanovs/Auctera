using MediatR;

namespace Auctera.Auctions.Application.Commands;

public record StopAuctionCommand(Guid AuctionId) : IRequest;
