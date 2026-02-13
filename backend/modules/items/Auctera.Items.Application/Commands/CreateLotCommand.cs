using Auctera.Shared.Domain.ValueObjects;

using MediatR;

namespace Auctera.Items.Application.Commands;

public sealed record CreateLotCommand(
    Guid SellerId,
    string Title,
    string Description,
    Money Price
) : IRequest<Guid>;
