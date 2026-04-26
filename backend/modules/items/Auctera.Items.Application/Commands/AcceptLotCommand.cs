using MediatR;

namespace Auctera.Items.Application.Commands;

public sealed record AcceptLotCommand(Guid LotId) : IRequest<Guid>;
