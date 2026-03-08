using Auctera.Items.Domain;
using Auctera.Persistance;

using Auctera.Items.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Auctera.Items.Infrastructure.Repository;
/// <summary>
/// Represents the lot repository class.
/// </summary>
public class LotRepository : ILotRepository
{
    private readonly AucteraDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="LotRepository"/> class.
    /// </summary>
    /// <param name="context">Context.</param>
    public LotRepository(AucteraDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets lot by id.
    /// </summary>
    /// <param name="id">Entity identifier.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that returns the operation result.</returns>
    public async Task<Lot?> GetLotById(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Lots.Include(l => l.Media).FirstOrDefaultAsync(l => l.Id == l.Id, cancellationToken);
    }

    /// <summary>
    /// Performs the save lot async operation.
    /// </summary>
    /// <param name="lot">Lot.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task SaveLotAsync(Lot lot, CancellationToken cancellationToken)
    {
        _context.Lots.Update(lot);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Adds lot async.
    /// </summary>
    /// <param name="lot">Lot.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task AddLotAsync(Lot lot, CancellationToken cancellationToken)
    {
        await _context.Lots.AddAsync(lot, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Deletes lot async.
    /// </summary>
    /// <param name="lot">Lot.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task DeleteLotAsync(Lot lot, CancellationToken cancellationToken)
    {
        _context.Remove(lot);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Gets queryable.
    /// </summary>
    /// <returns>The operation result.</returns>
    public IQueryable<Lot> GetQueryable()
    {
        return _context.Lots.Include(x => x.Media).AsQueryable().AsNoTracking();
    }
}
