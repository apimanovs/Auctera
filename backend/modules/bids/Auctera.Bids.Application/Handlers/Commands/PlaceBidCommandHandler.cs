using System.Threading;

using Auctera.Auctions.Application.Interfaces;
using Auctera.Bids.Application.Commands;
using Auctera.Bids.Application.Interfaces;
using Auctera.Items.Application.Interfaces;
using Auctera.Shared.Domain.Time;
using Auctera.Shared.Domain.ValueObjects;
using Auctera.Shared.Infrastructure.Interfaces;

using MediatR;

namespace Auctera.Bids.Application.Handlers.Commands;

/// <summary>
/// Represents the place bid command handler class.
/// </summary>
public sealed class PlaceBidCommandHandler
    : IRequestHandler<PlaceBidCommand>
{
    private readonly IAuctionRepository _auctionRepository;
    private readonly IBidRepository _bidRepository;
    private readonly ILotRepository _lotRepository;
    private readonly IDomainEventDispatcher _domainEventHandler;
    private readonly IClock _clock;

    public PlaceBidCommandHandler(
        IAuctionRepository auctionRepository,
        IBidRepository bidRepository,
        IDomainEventDispatcher domainEventHandler,
        IClock clock,
        ILotRepository lotRepository)
    {
        _auctionRepository = auctionRepository;
        _domainEventHandler = domainEventHandler;
        _clock = clock;
        _lotRepository = lotRepository;
        _bidRepository = bidRepository;
    }

    public async Task Handle(PlaceBidCommand request, CancellationToken cancellationToken)
    {
        if (request.BidderId == Guid.Empty)
        {
            throw new ArgumentException("BidderId is required.", nameof(request.BidderId));
        }

        var auction = await _auctionRepository.GetAuctionById(request.AuctionId, cancellationToken);

        if (auction == null)
        {
            throw new InvalidOperationException("Auction not found");
        }

        var lot = await _lotRepository.GetLotById(auction.LotId, cancellationToken);

        if (lot is null)
        {
            throw new InvalidOperationException("Auction lot not found");
        }

        if (lot.SellerId == request.BidderId)
        {
            throw new InvalidOperationException("You cannot bid on your own auction.");
        }

        auction.PlaceBid(
            Guid.NewGuid(),
            request.BidderId,
            new Money(request.Amount, request.Currency),
            _clock.UtcNow
        );

        var newActiveBid = auction.Bids.Where(b => b.Status == Shared.Domain.Enums.BidStatus.Active).FirstOrDefault(b => b.AuctionId == auction.Id);

        if (newActiveBid != null)
        {
            await _bidRepository.AddBidAsync(newActiveBid, cancellationToken);
        }

        await _auctionRepository.SaveAuctionAsync(auction, cancellationToken);
        await _domainEventHandler.DispatchAsync(auction.DomainEvents, cancellationToken);

        auction.ClearDomainEvents();
    }
}
