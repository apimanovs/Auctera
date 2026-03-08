using Auctera.Auctions.Application.Commands;
using Auctera.Auctions.Application.Interfaces;
using Auctera.Shared.Domain.Enums;
using Auctera.Shared.Domain.Time;
using Auctera.Shared.Infrastructure.Interfaces;

using MediatR;

namespace Auctera.Auctions.Application.Handlers.Commands;

/// <summary>
/// Represents the start auction command handler class.
/// </summary>
public sealed class StartAuctionCommandHandler : IRequestHandler<StartAuctionCommand>
{
    private readonly IAuctionRepository _auctionRepository;
    private readonly IDomainEventDispatcher _domainEventHandler;
    private readonly IClock _clock;

    /// <summary>
    /// Initializes a new instance of the <see cref="StartAuctionCommandHandler"/> class.
    /// </summary>
    /// <param name="auctionRepository">Auction repository.</param>
    /// <param name="clock">Clock.</param>
    /// <param name="domainEventDispatcher">Domain event dispatcher.</param>
    public StartAuctionCommandHandler(IAuctionRepository auctionRepository, IClock clock, IDomainEventDispatcher domainEventDispatcher)
    {
        _auctionRepository = auctionRepository;
        _clock = clock;
        _domainEventHandler = domainEventDispatcher;
    }
    /// <summary>
    /// Handles the operation.
    /// </summary>
    /// <param name="request">Input data for the operation.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Handle(StartAuctionCommand request, CancellationToken cancellationToken)
    {
        var auction = await _auctionRepository.GetAuctionById(request.AuctionId, cancellationToken);

        if (auction == null)
        {
            throw new InvalidOperationException("Auction not found.");
        }

        auction.StartAuction(_clock.UtcNow, MapDuration(request.Duration));

        await _auctionRepository.SaveAuctionAsync(auction, cancellationToken);
        await _domainEventHandler.DispatchAsync(auction.DomainEvents, cancellationToken);
        auction.ClearDomainEvents();
    }

    /// <summary>
    /// Performs the map duration operation.
    /// </summary>
    /// <param name="option">Option.</param>
    /// <returns>The operation result.</returns>
    private TimeSpan MapDuration(AuctionDurationOption option)
    {
        return option switch
        {
            AuctionDurationOption.Flash1Hour => TimeSpan.FromHours(1),
            AuctionDurationOption.Flash6Hours => TimeSpan.FromHours(6),
            AuctionDurationOption.Classic3Days => TimeSpan.FromDays(3),
            AuctionDurationOption.Classic7Days => TimeSpan.FromDays(7),
            AuctionDurationOption.Classic14Days => TimeSpan.FromDays(14),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
