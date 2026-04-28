using Auctera.Items.Application.Commands;
using Auctera.Items.Application.Interfaces;
using Auctera.Shared.Domain.Enums;

using MediatR;

namespace Auctera.Items.Application.Handlers.Commands;

/// <summary>
/// Represents the delete lot command handler class.
/// </summary>
public sealed class DeleteLotCommandHandler(ILotRepository lotRepository) : IRequestHandler<DeleteLotCommand>
{
    private readonly ILotRepository _lotRepository = lotRepository;

    /// <summary>
    /// Handles the operation.
    /// </summary>
    /// <param name="command">Input data for the operation.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Handle(DeleteLotCommand command, CancellationToken cancellationToken)
    {
        var lotToDelete = await _lotRepository.GetLotById(command.lotId, cancellationToken);

        if (lotToDelete is null)
        {
            throw new KeyNotFoundException($"Lot {command.lotId} not found.");
        }

        if (lotToDelete.SellerId != command.sellerId)
        {
            throw new UnauthorizedAccessException("Only the seller can delete this lot.");
        }

        if (lotToDelete.Status != LotStatus.Draft)
        {
            throw new InvalidOperationException("Only draft lots can be deleted.");
        }

        await _lotRepository.DeleteLotAsync(lotToDelete, cancellationToken);
    }
}
