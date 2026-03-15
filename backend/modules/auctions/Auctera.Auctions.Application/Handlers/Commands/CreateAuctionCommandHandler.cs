using Auctera.Auctions.Application.Commands;
using Auctera.Auctions.Application.Interfaces;
using Auctera.Auctions.Domain;
using Auctera.Items.Application.Interfaces;
using Auctera.Shared.Infrastructure.Interfaces;

using MediatR;

namespace Auctera.Auctions.Application.Handlers.Commands;

/// <summary>
/// Represents the create auction command handler class.
/// </summary>
public sealed class CreateAuctionCommandHandler
    (
        IAuctionRepository auctionRepository,
        ILotRepository lotRepository,
        IDomainEventDispatcher domainEventDispatcher
    ) : IRequestHandler<CreateAuctionCommand, Guid>
{
    private readonly IAuctionRepository _auctionRepository = auctionRepository;
    private readonly IDomainEventDispatcher _domainEventHandler = domainEventDispatcher;
    private readonly ILotRepository _lotRepository = lotRepository;

    /// <summary>
    /// Handles the operation.
    /// </summary>
    /// <param name="command">Input data for the operation.</param>
    /// <param name="ct">Ct.</param>
    /// <returns>A task that returns the operation result.</returns>
    public async Task<Guid> Handle(CreateAuctionCommand command, CancellationToken ct)
    {
        var lot = await _lotRepository.GetLotById(command.LotId, ct);

        if (lot == null)
        {
            throw new Exception("Lot doesnt exist");
        }

        var auction = new Auction(Guid.NewGuid());

        auction.AddLot(lot);

        await _auctionRepository.AddAuctionAsync(auction, ct);
        await _domainEventHandler.DispatchAsync(auction.DomainEvents, ct);
        auction.ClearDomainEvents();

        return auction.Id;
    }
}
