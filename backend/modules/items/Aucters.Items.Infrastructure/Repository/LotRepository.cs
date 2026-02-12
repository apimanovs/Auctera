using Auctera.Items.Domain;
using Auctera.Persistance;

using Auctera.Items.Application.Interfaces;

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
        return await _context.Lots.FindAsync(new object[] { id }, cancellationToken);
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
}
