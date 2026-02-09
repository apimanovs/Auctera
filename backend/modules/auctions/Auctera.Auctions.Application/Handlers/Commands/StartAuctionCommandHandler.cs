using Auctera.Auctions.Application.Commands;
using Auctera.Auctions.Application.Interfaces;
using Auctera.Shared.Domain.Time;
using Auctera.Shared.Infrastructure.Interfaces;

using MediatR;

namespace Auctera.Auctions.Application.Handlers.Commands;

public sealed class StartAuctionCommandHandler : IRequestHandler<StartAuctionCommand>
{
    private readonly IAuctionRepository _auctionRepository;
    private readonly IDomainEventDispatcher _domainEventHandler;
    private readonly IClock _clock;

    public StartAuctionCommandHandler(IAuctionRepository auctionRepository, IClock clock)
    {
        _auctionRepository = auctionRepository;
        _clock = clock;
    }
    public async Task Handle(StartAuctionCommand request, CancellationToken cancellationToken)
    {
        var auction = await _auctionRepository.GetAuctionById(request.AuctionId, cancellationToken);

        if (auction == null)
        {
            throw new InvalidOperationException("Auction not found.");
        }

        auction.StartAuction(_clock.UtcNow, request.TimeSpan);

        await _auctionRepository.SaveAuctionAsync(auction, cancellationToken);
        await _domainEventHandler.DispatchAsync(auction.DomainEvents, cancellationToken);
    }
}
