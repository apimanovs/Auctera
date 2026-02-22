using Auctera.Identity.Application.Interfaces;
using Auctera.Identity.Domain;
using Auctera.Persistance;

using Microsoft.EntityFrameworkCore;

namespace Auctera.Identity.Infrastructure.Repository;
public class UserRepository(AucteraDbContext context) : IUserRepository
{
    private readonly AucteraDbContext _context = context;

    public Task AddUserAsync(User user)
    {
        _context.Users.Add(user);
        return _context.SaveChangesAsync();
    }

    public Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        return _context.SaveChangesAsync();
    }

    public Task DeleteUserAsync(User user)
    {
        _context.Users.Remove(user);
        return _context.SaveChangesAsync();
    }

    public Task<User> GetUserByEmailAsync(string email)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public Task<User> GetUserByIdAsync(Guid id)
    {
        return _context.Users.FindAsync(id).AsTask();
    }
}
