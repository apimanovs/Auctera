using Auctera.Items.Application.Commands;
using Auctera.Items.Application.Interfaces;
using Auctera.Items.Domain;
using Auctera.Shared.Domain.ValueObjects;

using MediatR;

namespace Auctera.Items.Application.Handlers.Commands;

/// <summary>
/// Represents the create lot command handler class.
/// </summary>
public sealed class CreateLotCommandHandler : IRequestHandler<CreateLotCommand, Guid>
{
    private readonly ILotRepository _lotRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateLotCommandHandler"/> class.
    /// </summary>
    /// <param name="lotRepository">Lot repository.</param>
    public CreateLotCommandHandler(ILotRepository lotRepository)
    {
        _lotRepository = lotRepository;
    }

    /// <summary>
    /// Handles the operation.
    /// </summary>
    /// <param name="request">Input data for the operation.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that returns the operation result.</returns>
    public async Task<Guid> Handle(CreateLotCommand request, CancellationToken cancellationToken)
    {
        var money = new Money(request.Amount, request.Currency);

        Lot lot = new(
            Guid.NewGuid(),
            request.SellerId,
            request.Title,
            request.Description,
            money,
            request.Category,
            request.Gender,
            request.Size,
            request.Brand,
            request.Condition,
            request.Color
        );

        foreach (var key in request.PhotoKeys ?? [])
        {
            lot.AddPhoto(key);
        }

        await _lotRepository.AddLotAsync(lot, cancellationToken);
        return lot.Id;
    }
}
