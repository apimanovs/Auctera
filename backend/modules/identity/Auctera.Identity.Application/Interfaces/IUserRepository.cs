using Auctera.Identity.Domain;

namespace Auctera.Identity.Application.Interfaces;

/// <summary>
/// Represents the i user repository interface.
/// </summary>
public interface IUserRepository
{
    Task<User> GetUserByEmailAsync(string email);
    Task<User> GetUserByIdAsync(Guid id);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(User user);
}
