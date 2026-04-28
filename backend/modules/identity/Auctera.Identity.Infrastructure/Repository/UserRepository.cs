
using Auctera.Identity.Application.Interfaces;
using Auctera.Identity.Application.Models;
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
    public Task<User?> GetUserByEmailAsync(string email)
    {
        return _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
    }

    public Task<User?> GetUserByUsernameAsync(string username)
    {
        return _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserName == username);
    }

    public async Task<IReadOnlyList<User>> GetUsersByIdsAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken)
    {
        if (ids.Count == 0)
        {
            return [];
        }

        return await _context.Users
            .AsNoTracking()
            .Where(u => ids.Contains(u.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Guid>> GetUserIdsByLocationAsync(
        string? city,
        string? country,
        string? location,
        CancellationToken cancellationToken)
    {
        var query = _context.Users.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(city))
        {
            var normalizedCity = city.Trim().ToLower();
            query = query.Where(u => u.City != null && u.City.ToLower().Contains(normalizedCity));
        }

        if (!string.IsNullOrWhiteSpace(country))
        {
            var normalizedCountry = country.Trim().ToLower();
            query = query.Where(u => u.Country != null && u.Country.ToLower().Contains(normalizedCountry));
        }

        if (!string.IsNullOrWhiteSpace(location))
        {
            var normalizedLocation = location.Trim().ToLower();
            query = query.Where(u =>
                (u.City != null && u.City.ToLower().Contains(normalizedLocation)) ||
                (u.Country != null && u.Country.ToLower().Contains(normalizedLocation)));
        }

        return await query.Select(u => u.Id).ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Gets user by id async.
    /// </summary>
    /// <param name="id">Entity identifier.</param>
    /// <returns>A task that returns the operation result.</returns>
    public Task<User?> GetUserByIdAsync(Guid id)
    {
        return _context.Users.FindAsync(id).AsTask();
    }

    // queries with data
    public Task<int> GetUsersBidsPlacedCount(Guid id)
    {
        return _context.Bids.CountAsync(b => b.BidderId == id);
    }

    public Task<int> GetUserActiveLotsCountAsync(Guid id)
    {
        return _context.Lots.CountAsync(l => l.SellerId == id && l.Status == Shared.Domain.Enums.LotStatus.Published);
    }

    public Task<int> GetUserSoldLotsCountAsync(Guid id)
    {
        return _context.Lots.AsNoTracking().Where(l => l.SellerId == id && l.Status == Shared.Domain.Enums.LotStatus.Sold).CountAsync();
    }

    public Task<List<UserProfileListingDto>> GetUserActiveLotsAsync(Guid userId, int take = 4)
    {
        return _context.Lots.AsNoTracking()
            .Where(l => l.SellerId == userId && l.Status == Shared.Domain.Enums.LotStatus.Published)
            .Take(take)
            .Select(l => new UserProfileListingDto
            {
                LotId = l.Id,
                Title = l.Title,
                Brand = l.Brand,
                CurrentPrice = l.Price.Amount,
                Currency = l.Price.Currency,
                ThumbnailUrl = l.Media.FirstOrDefault() != null ? l.Media.FirstOrDefault().Key : null,
                Status = l.Status
            })
            .ToListAsync();
    }

    public Task<List<UserProfileListingDto>> GetUserSoldLotsAsync(Guid userId, int take = 4)
    {
        return _context.Lots.AsNoTracking()
            .Where(l => l.SellerId == userId && l.Status == Shared.Domain.Enums.LotStatus.Sold)
            .Take(take)
            .Select(l => new UserProfileListingDto
            {
                LotId = l.Id,
                Title = l.Title,
                Brand = l.Brand,
                CurrentPrice = l.Price.Amount,
                Currency = l.Price.Currency,
                ThumbnailUrl = l.Media.FirstOrDefault() != null ? l.Media.FirstOrDefault().Key : null,
                Status = l.Status
            })
            .ToListAsync();
    }
}
