using Auctera.Items.Application.Models;
using Auctera.Items.Domain.Enums;

using MediatR;

namespace Auctera.Items.Application.Queries;

public sealed record GetLotsListQuery(
    LotCategory? Category,
    LotGender? Gender,
    LotSize? Size,
    string? Brand
) : IRequest<List<LotDto>>;
