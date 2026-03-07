using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using Auctera.Items.Application.Commands;
using Auctera.Items.Application.Interfaces;

namespace Auctera.Items.Application.Handlers.Commands;
public sealed class PublishLotCommandHandler : IRequestHandler<PublishLotCommand, Guid>
{
    private readonly ILotRepository _lotRepository;

    public PublishLotCommandHandler(ILotRepository lotRepository)
    {
        _lotRepository = lotRepository;
    }

    public async Task<Guid> Handle(PublishLotCommand request, CancellationToken cancellationToken)
    {
        var lot = await _lotRepository.GetLotById(request.LotId, cancellationToken);

        if (lot == null)
        {
            throw new InvalidOperationException("Lot not found.");
        }

        if (lot.SellerId != request.SellerId)
        {
            throw new UnauthorizedAccessException("Seller does not own the lot.");
        }

        lot.Publish();

        await _lotRepository.SaveLotAsync(lot, cancellationToken);
        return lot.Id;
    }
}
