using Auctera.Items.Domain;
using Auctera.Persistance;

using Auctera.Items.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Auctera.Items.Infrastructure.Repository;
public class LotRepository : ILotRepository
{
    private readonly AucteraDbContext _context;

    public LotRepository(AucteraDbContext context)
    {
        _context = context;
    }

    public async Task<Lot?> GetLotById(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Lots.Include(l => l.Media).FirstOrDefaultAsync(l => l.Id == l.Id, cancellationToken);
    }

    public async Task SaveLotAsync(Lot lot, CancellationToken cancellationToken)
    {
        _context.Lots.Update(lot);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddLotAsync(Lot lot, CancellationToken cancellationToken)
    {
        await _context.Lots.AddAsync(lot, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteLotAsync(Lot lot, CancellationToken cancellationToken)
    {
        _context.Remove(lot);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public IQueryable<Lot> GetQueryable()
    {
        return _context.Lots.Include(x => x.Media).AsQueryable().AsNoTracking();
    }
}
