using System.Threading;

using Auctera.Identity.Application.Interfaces;
using Auctera.Identity.Domain;
using Auctera.Persistance;

using Microsoft.EntityFrameworkCore;

namespace Auctera.Identity.Infrastructure.Repository;

public sealed class RefreshTokenRepository(AucteraDbContext context) : IRefreshTokenRepository
{
    private readonly AucteraDbContext _context = context;

    public Task SaveRefreshTokenAsync(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Update(refreshToken);
        return _context.SaveChangesAsync();
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        return await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == token);
    }

    public Task DeleteRefreshTokenAsync(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Remove(refreshToken);
        return _context.SaveChangesAsync();
    }

    public Task AddRefreshToken(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Add(refreshToken);
        return _context.SaveChangesAsync();
    }

    public async Task RevokeRefreshTokenAsync(RefreshToken refreshToken)
    {
        var existingToken = await _context.RefreshTokens.FindAsync(refreshToken.UserId);
         
        if (existingToken != null)
        {
            existingToken.Revoke();
            _context.RefreshTokens.Update(existingToken);
            await _context.SaveChangesAsync();
        }
    }
}
