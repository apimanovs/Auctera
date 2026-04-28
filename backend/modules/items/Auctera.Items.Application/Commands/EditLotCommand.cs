using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Auctera.Shared.Domain.ValueObjects;
using Auctera.Items.Domain.Enums;

using MediatR;

namespace Auctera.Items.Application.Commands;

/// <summary>
/// Represents the edit lot command record.
/// </summary>
public record EditLotCommand
(
    Guid id,
    Guid sellerId,
    string title,
    string description,
    Money price,
    LotSize size,
    string brand,
    LotCategory category,
    LotGender gender,
    LotCondition condition,
    string? color
)
    : IRequest;
