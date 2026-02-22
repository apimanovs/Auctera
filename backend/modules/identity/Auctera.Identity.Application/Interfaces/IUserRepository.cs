using Auctera.Identity.Domain;

namespace Auctera.Identity.Application.Interfaces;

public interface IUserRepository
{
    Task<User> GetUserByEmailAsync(string email);
    Task<User> GetUserByIdAsync(Guid id);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(User user);
}
