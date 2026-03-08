using Auctera.Identity.Application.Interfaces;
using Auctera.Identity.Domain;
using Auctera.Persistance;

using Microsoft.EntityFrameworkCore;

namespace Auctera.Identity.Infrastructure.Repository;
/// <summary>
/// Represents the user repository class.
/// </summary>
public class UserRepository(AucteraDbContext context) : IUserRepository
{
    private readonly AucteraDbContext _context = context;

    /// <summary>
    /// Adds user async.
    /// </summary>
    /// <param name="user">User.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task AddUserAsync(User user)
    {
        _context.Users.Add(user);
        return _context.SaveChangesAsync();
    }

    /// <summary>
    /// Updates user async.
    /// </summary>
    /// <param name="user">User.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        return _context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes user async.
    /// </summary>
    /// <param name="user">User.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task DeleteUserAsync(User user)
    {
        _context.Users.Remove(user);
        return _context.SaveChangesAsync();
    }

    /// <summary>
    /// Gets user by email async.
    /// </summary>
    /// <param name="email">Email.</param>
    /// <returns>A task that returns the operation result.</returns>
    public Task<User> GetUserByEmailAsync(string email)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    /// <summary>
    /// Gets user by id async.
    /// </summary>
    /// <param name="id">Entity identifier.</param>
    /// <returns>A task that returns the operation result.</returns>
    public Task<User> GetUserByIdAsync(Guid id)
    {
        return _context.Users.FindAsync(id).AsTask();
    }
}
