namespace Auctera.Identity.Application.Models;

public sealed record AuthResult(string AccessToken, string RefreshToken);
