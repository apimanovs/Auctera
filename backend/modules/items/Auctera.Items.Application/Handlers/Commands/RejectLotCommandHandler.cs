using Auctera.Items.Application.Commands;
using Auctera.Items.Application.Interfaces;
using MediatR;

namespace Auctera.Items.Application.Handlers.Commands;

public sealed class RejectLotCommandHandler(ILotRepository lotRepository)
    : IRequestHandler<RejectLotCommand, Guid>
{
    private readonly ILotRepository _lotRepository = lotRepository;

    public async Task<Guid> Handle(RejectLotCommand request, CancellationToken cancellationToken)
    {
        var lot = await _lotRepository.GetLotById(request.LotId, cancellationToken)
            ?? throw new KeyNotFoundException($"Lot {request.LotId} not found.");

        await _lotRepository.DeleteLotAsync(lot, cancellationToken);

        return request.LotId;
    }
}
