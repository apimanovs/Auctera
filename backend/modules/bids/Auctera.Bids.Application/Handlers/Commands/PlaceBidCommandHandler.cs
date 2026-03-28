using System.Threading;

using Auctera.Auctions.Application.Interfaces;
using Auctera.Bids.Application.Commands;
using Auctera.Items.Application.Interfaces;
using Auctera.Shared.Domain.Time;
using Auctera.Shared.Domain.ValueObjects;
using Auctera.Shared.Infrastructure.Interfaces;

using MediatR;

using Microsoft.CodeAnalysis;

namespace Auctera.Bids.Application.Handlers.Commands;

/// <summary>
/// Represents the place bid command handler class.
/// </summary>
public sealed class PlaceBidCommandHandler
    : IRequestHandler<PlaceBidCommand>
{
    private readonly IAuctionRepository _auctionRepository;
    private readonly ILotRepository _lotRepository;
    private readonly IDomainEventDispatcher _domainEventHandler;
    private readonly IClock _clock;

    public PlaceBidCommandHandler(
        IAuctionRepository auctionRepository,
        IDomainEventDispatcher domainEventHandler,
        IClock clock,
        ILotRepository lotRepository)
    {
        _auctionRepository = auctionRepository;
        _domainEventHandler = domainEventHandler;
        _clock = clock;
        _lotRepository = lotRepository;
    }

    public async Task Handle(PlaceBidCommand request, CancellationToken cancellationToken)
    {
        if (request.BidderId == Guid.Empty)
        {
            throw new ArgumentException("BidderId is required.", nameof(request.BidderId));
        }

        var auction = await _auctionRepository.GetAuctionById(request.AuctionId, cancellationToken);

        var lot = await _lotRepository.GetLotById(auction.LotId, cancellationToken);

        //if (lot.SellerId == request.BidderId)
        //{
        //    throw new InvalidOperationException("You cant bid on your own auction.");
        //}

        if (auction == null)
        {
            throw new InvalidOperationException("Auction not found");
        }

        auction.PlaceBid(
            Guid.NewGuid(),
            request.BidderId,
            new Money(request.Amount, request.Currency),
            _clock.UtcNow
        );

        await _auctionRepository.SaveAuctionAsync(auction, cancellationToken);
        await _domainEventHandler.DispatchAsync(auction.DomainEvents, cancellationToken);

        auction.ClearDomainEvents();
    }
}
