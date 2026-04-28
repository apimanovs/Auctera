using Auctera.Identity.Application.Models;
using Auctera.Identity.Domain;

namespace Auctera.Identity.Application.Interfaces;

/// <summary>
/// Represents the user repository interface.
/// </summary>
public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByIdAsync(Guid id);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<IReadOnlyList<User>> GetUsersByIdsAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken);
    Task<IReadOnlyList<Guid>> GetUserIdsByLocationAsync(string? city, string? country, string? location, CancellationToken cancellationToken);

    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(User user);

    Task<int> GetUsersBidsPlacedCount(Guid userId);
    Task<int> GetUserActiveLotsCountAsync(Guid userId);
    Task<int> GetUserSoldLotsCountAsync(Guid userId);

    Task<List<UserProfileListingDto>> GetUserActiveLotsAsync(Guid userId, int take = 4);
    Task<List<UserProfileListingDto>> GetUserSoldLotsAsync(Guid userId, int take = 4);
}
