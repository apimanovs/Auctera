using Auctera.Identity.Application.Models;

using MediatR;

namespace Auctera.Identity.Application.Commands;

public sealed record CurrentUserCommand(Guid userId) : IRequest<UserDto>;
