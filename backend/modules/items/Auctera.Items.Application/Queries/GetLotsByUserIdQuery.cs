using Auctera.Items.Application.Models;
using MediatR;

namespace Auctera.Items.Application.Queries;

public sealed record GetLotsByUserIdQuery(Guid UserId) : IRequest<IReadOnlyList<MyLotListItemDto>>;
