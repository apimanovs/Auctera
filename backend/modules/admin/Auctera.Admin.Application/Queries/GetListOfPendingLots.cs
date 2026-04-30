using Auctera.Admin.Domain.Models;

using MediatR;

namespace Auctera.Admin.Application.Queries;

public sealed record GetListOfPendingLots(Guid adminId) : IRequest<IReadOnlyList<PendingLotPreviewDto>>;
