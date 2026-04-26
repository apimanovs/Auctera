using MediatR;

namespace Auctera.Items.Application.Commands;

public sealed record RejectLotCommand(Guid LotId) : IRequest<Guid>;
