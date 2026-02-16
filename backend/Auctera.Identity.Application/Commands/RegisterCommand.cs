using MediatR;

namespace Auctera.Identity.Application.Commands;

public record RegisterCommand
(
    string username,
    string email,
    string password,
    string confirmPassword
) : IRequest<string>;
