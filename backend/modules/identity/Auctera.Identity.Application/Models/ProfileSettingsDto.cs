namespace Auctera.Identity.Application.Models;

public sealed class ProfileSettingsDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public string Username { get; init; } = default!;
    public string Email { get; init; } = default!;
    public string? City { get; init; }
    public string? Country { get; init; }
}
