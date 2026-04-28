using Auctera.Items.Application.Models;
using Auctera.Items.Domain.Enums;
using Auctera.Shared.Domain.Enums;

using MediatR;

namespace Auctera.Items.Application.Queries;

/// <summary>
/// Represents the get lots list query record.
/// </summary>
public sealed record GetLotsListQuery(
    string? Search,
    LotCategory? Category,
    LotGender? Gender,
    LotStatus? Status,
    LotSize? Size,
    string? Brand,
    LotCondition? Condition,
    decimal? MinPrice,
    decimal? MaxPrice,
    int? MinYear,
    int? MaxYear,
    string? City,
    string? Country,
    string? Location
) : IRequest<List<LotPreviewDto>>;
