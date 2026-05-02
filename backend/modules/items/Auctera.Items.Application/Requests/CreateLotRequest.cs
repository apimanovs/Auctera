using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Auctera.Items.Domain.Enums;

using MediatR;

namespace Auctera.Items.Application.Requests;

public sealed record CreateLotRequest
(
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
    string? Country,
    string? City,
    string Age,
    string Style,
    IReadOnlyList<string>? PhotoKeys
) : IRequest<Guid>;
