using MediatR;

namespace Auctera.Admin.Application.Commands;

public sealed record RejectLotCommand(Guid lotId, Guid adminId) : IRequest;

