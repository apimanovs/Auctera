using Auctera.Items.Application.Models;
using MediatR;

namespace Auctera.Items.Application.Queries;

public record GetLotQuery(Guid lotId) : IRequest<LotDto>;
