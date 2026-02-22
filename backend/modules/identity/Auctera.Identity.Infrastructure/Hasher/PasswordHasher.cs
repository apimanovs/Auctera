using Auctera.Identity.Application.Interfaces;

using BCrypt.Net;

namespace Auctera.Identity.Infrastructure.Hasher;

internal class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 16);
    }
    public bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
    }
}
