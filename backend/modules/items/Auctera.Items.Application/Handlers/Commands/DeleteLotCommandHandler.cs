using Auctera.Items.Application.Commands;
using Auctera.Items.Application.Interfaces;
using Auctera.Shared.Domain.Enums;
using Auctera.Shared.Infrastructure.Media;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Auctera.Items.Application.Handlers.Commands;

/// <summary>
/// Represents the delete lot command handler class.
/// </summary>
public sealed class DeleteLotCommandHandler(
    ILotRepository lotRepository,
    IMediaUploader mediaUploader,
    ILogger<DeleteLotCommandHandler> logger) : IRequestHandler<DeleteLotCommand>
{
    private readonly ILotRepository _lotRepository = lotRepository;
    private readonly IMediaUploader _mediaUploader = mediaUploader;
    private readonly ILogger<DeleteLotCommandHandler> _logger = logger;

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

        var mediaKeys = lotToDelete.Media
            .Where(media => media.Type == "photo")
            .Select(media => media.Key)
            .Where(key => !string.IsNullOrWhiteSpace(key))
            .Distinct(StringComparer.Ordinal)
            .ToList();

        var keysUsedByOtherLots = await _lotRepository.GetMediaKeysUsedByOtherLotsAsync(
            lotToDelete.Id,
            mediaKeys,
            cancellationToken);

        var keysToDelete = mediaKeys
            .Except(keysUsedByOtherLots, StringComparer.Ordinal)
            .ToList();

        await _lotRepository.DeleteLotAsync(lotToDelete, cancellationToken);

        try
        {
            await _mediaUploader.DeleteManyAsync(keysToDelete, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception exception)
        {
            _logger.LogError(
                exception,
                "Failed to delete media from storage for lot {LotId}. Keys: {MediaKeys}",
                lotToDelete.Id,
                keysToDelete);
        }
    }
}
