using Auctera.Auctions.Application.Commands;
using Auctera.Auctions.Application.Interfaces;
using Auctera.Auctions.Domain;
using Auctera.Items.Application.Interfaces;
using Auctera.Shared.Infrastructure.Interfaces;

using MediatR;

namespace Auctera.Auctions.Application.Handlers.Commands;

public sealed class CreateAuctionCommandHandler : IRequestHandler<CreateAuctionCommand, Guid>
{
    private readonly IAuctionRepository _auctionRepository;
    private readonly IDomainEventDispatcher _domainEventHandler;
    private readonly ILotRepository _lotRepository;

    public CreateAuctionCommandHandler(IAuctionRepository auctionRepository, ILotRepository lotRepository)
    {
        _auctionRepository = auctionRepository;
        _lotRepository = lotRepository;
    }

    public async Task<Guid> Handle(CreateAuctionCommand cmd, CancellationToken ct)
    {
        var lot = await _lotRepository.GetLotById(cmd.LotId, ct);

        var auction = new Auction(Guid.NewGuid());
        auction.AddLot(lot);

        await _auctionRepository.AddAuctionAsync(auction, ct);
        await _domainEventHandler.DispatchAsync(auction.DomainEvents, ct);

        return auction.Id;
    }
}
