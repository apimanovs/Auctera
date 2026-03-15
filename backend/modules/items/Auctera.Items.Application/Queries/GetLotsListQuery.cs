using Auctera.Items.Application.Models;
using Auctera.Items.Domain.Enums;

using MediatR;

namespace Auctera.Items.Application.Queries;

/// <summary>
/// Represents the get lots list query record.
/// </summary>
public sealed record GetLotsListQuery(
    LotCategory? Category,
    LotGender? Gender,
    LotSize? Size,
    string? Brand
) : IRequest<List<LotDto>>;
