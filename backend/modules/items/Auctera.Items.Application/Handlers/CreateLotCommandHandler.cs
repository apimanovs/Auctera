using MediatR;
using Auctera.Items.Application.Commands;
using Auctera.Items.Domain;
using Auctera.Items.Application.Interfaces;

namespace Auctera.Items.Application.Handlers;

public sealed class CreateLotCommandHandler : IRequestHandler<CreateLotCommand, Guid>   
{
    private readonly ILotRepository _lotRepository;

    public CreateLotCommandHandler(ILotRepository lotRepository)
    {
        _lotRepository = lotRepository;
    }

    public async Task<Guid> Handle(CreateLotCommand request, CancellationToken cancellationToken)
    {
        Lot lot = new(
            Guid.NewGuid(),
            request.SellerId,
            request.AuctionId,
            request.Title,
            request.Description,
            request.Price
        );

        await _lotRepository.AddLotAsync(lot, cancellationToken);
        return lot.Id;
    }
}
