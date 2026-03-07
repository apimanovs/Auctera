using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Auctera.Items.Application.Commands;
using Auctera.Items.Application.Interfaces;
using Auctera.Items.Domain.Enums;
using Auctera.Shared.Domain.Enums;
using Auctera.Shared.Domain.ValueObjects;

using MediatR;


namespace Auctera.Items.Application.Handlers.Commands;

public sealed class EditLotCommandHandler : IRequestHandler<EditLotCommand>
{
    private readonly ILotRepository _lotRepository;

    public EditLotCommandHandler(ILotRepository lotRepository)
    {
        _lotRepository = lotRepository;
    }

    public async Task Handle(EditLotCommand request, CancellationToken cancellationToken)
    {
        var lot = await _lotRepository.GetLotById(request.id, cancellationToken);

        if (lot is null)
        {
            throw new KeyNotFoundException($"Lot {request.id} not found.");
        }

        lot.Edit(request.sellerId,
            request.title,
            request.description,
            request.price,
            request.category,
            request.gender,
            request.size,
            request.brand,
            request.condition,
            request.color
        );

        await _lotRepository.SaveLotAsync(lot, cancellationToken);
    }
}
