using MediatR;

namespace Auctera.Identity.Application.Commands;

public sealed record LogoutCommand(string? RefreshToken) : IRequest;
