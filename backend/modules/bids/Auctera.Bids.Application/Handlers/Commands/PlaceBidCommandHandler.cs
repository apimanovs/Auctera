using Auctera.Auctions.Application.Interfaces;
using Auctera.Bids.Application.Commands;
using Auctera.Shared.Domain.Time;
using Auctera.Shared.Domain.ValueObjects;

using MediatR;
using Auctera.Shared.Infrastructure.Interfaces;

namespace Auctera.Bids.Application.Handlers.Commands;

public sealed class PlaceBidCommandHandler
    : IRequestHandler<PlaceBidCommand>
{
    private readonly IAuctionRepository _auctionRepository;
    private readonly IDomainEventDispatcher _domainEventHandler;
    private readonly IClock _clock;

    public PlaceBidCommandHandler(
        IAuctionRepository auctionRepository,
        IDomainEventDispatcher domainEventHandler,
        IClock clock)
    {
        _auctionRepository = auctionRepository;
        _domainEventHandler = domainEventHandler;
        _clock = clock;
    }

    public async Task Handle(
        PlaceBidCommand request,
        CancellationToken ct)
    {
        if (request.BidderId == Guid.Empty)
        {
            throw new ArgumentException("BidderId is required.", nameof(request.BidderId));
        }

        var auction = await _auctionRepository
            .GetAuctionById(request.AuctionId, ct);

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

        await _auctionRepository.SaveAuctionAsync(auction, ct);
        await _domainEventHandler.DispatchAsync(auction.DomainEvents, ct);

        auction.ClearDomainEvents();
    }
}
