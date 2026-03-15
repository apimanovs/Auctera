using Auctera.Auctions.Application.Commands;
using Auctera.Auctions.Application.Interfaces;
using Auctera.Shared.Domain.Time;
using Auctera.Shared.Infrastructure.Interfaces;
using Auctera.Shared.Domain.Abstractions;

using MediatR;

namespace Auctera.Auctions.Application.Handlers.Commands;
/// <summary>
/// Represents the stop auction command handler class.
/// </summary>
public sealed class StopAuctionCommandHandler : IRequestHandler<StopAuctionCommand>
{
    private readonly IAuctionRepository _auctionRepository;
    private readonly IDomainEventDispatcher _domainEventHandler;
    private readonly IClock _clock;

    public StopAuctionCommandHandler(
        IAuctionRepository auctionRepository,
        IClock clock,
        IDomainEventDispatcher domainEventHandler)
    {
        _auctionRepository = auctionRepository;
        _clock = clock;
        _domainEventHandler = domainEventHandler;
    }

    /// <summary>
    /// Handles the operation.
    /// </summary>
    /// <param name="request">Input data for the operation.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Handle(StopAuctionCommand request, CancellationToken cancellationToken)
    {
        var auction = await _auctionRepository.GetAuctionById(request.AuctionId, cancellationToken);

        if (auction == null)
        {
            throw new InvalidOperationException("Auction not found.");
        }

        await auction.StopAuction(_clock.UtcNow);
        await _auctionRepository.SaveAuctionAsync(auction, cancellationToken);
        await _domainEventHandler.DispatchAsync(auction.DomainEvents, cancellationToken);
        auction.ClearDomainEvents();
    }
}
