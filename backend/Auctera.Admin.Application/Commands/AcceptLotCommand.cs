using Auctera.Items.Domain;

using MediatR;

namespace Auctera.Admin.Application.Commands;

public sealed record AcceptLotCommand(Guid lotId, Guid adminId) : IRequest;
