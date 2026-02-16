using MediatR;

namespace Auctera.Identity.Application.Commands;

public record LoginCommand (string email, string password) : IRequest<string>;
