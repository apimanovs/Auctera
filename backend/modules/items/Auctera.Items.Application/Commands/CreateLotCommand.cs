using Auctera.Items.Domain.Enums;
using Auctera.Shared.Domain.ValueObjects;

using MediatR;

namespace Auctera.Items.Application.Commands;

public sealed record CreateLotCommand(
    Guid SellerId,
    string Title,
    string Description,
    decimal Amount,
    string Currency,
    LotCategory Category,
    LotGender Gender,
    LotSize Size,
    string Brand,
    LotCondition Condition,
    string? Color,
    IReadOnlyList<string>? PhotoKeys
) : IRequest<Guid>;
