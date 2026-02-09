using System;
using MediatR;

namespace Auctera.Auctions.Application.Commands;

public sealed record StartAuctionCommand(Guid AuctionId, TimeSpan TimeSpan) : IRequest;
