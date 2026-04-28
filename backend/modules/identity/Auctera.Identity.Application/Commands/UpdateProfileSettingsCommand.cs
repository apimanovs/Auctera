using Auctera.Identity.Application.Models;

using MediatR;

namespace Auctera.Identity.Application.Commands;

public sealed record UpdateProfileSettingsCommand(
    Guid UserId,
    string Name,
    string Username,
    string? City,
    string? Country
) : IRequest<ProfileSettingsDto>;
