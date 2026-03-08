using Auctera.Shared.Domain.Enums;

using MediatR;

namespace Auctera.Auctions.Application.Commands;

/// <summary>
/// Represents the start auction command record.
/// </summary>
public sealed record StartAuctionCommand(Guid AuctionId, AuctionDurationOption Duration) : IRequest;
