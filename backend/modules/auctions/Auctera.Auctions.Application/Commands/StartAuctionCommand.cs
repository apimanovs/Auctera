using Auctera.Shared.Domain.Enums;

using MediatR;

namespace Auctera.Auctions.Application.Commands;

public sealed record StartAuctionCommand(Guid AuctionId, AuctionDurationOption Duration) : IRequest;
