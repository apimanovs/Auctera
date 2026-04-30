using MediatR;

namespace Auctera.Admin.Application.Commands;

public sealed record DeleteLotCommand(Guid lotId, Guid adminId) : IRequest;
