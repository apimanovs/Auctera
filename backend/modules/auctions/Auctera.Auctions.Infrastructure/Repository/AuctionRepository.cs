using Auctera.Auctions.Application.Interfaces;
using Auctera.Persistance;
using Auctera.Auctions.Domain;

namespace Auctera.Auctions.Infrastructure.Repository;

/// <summary>
/// Represents the auction repository class.
/// </summary>
public class AuctionRepository : IAuctionRepository
{
    private readonly AucteraDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuctionRepository"/> class.
    /// </summary>
    /// <param name="context">Context.</param>
    public AuctionRepository(AucteraDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets auction by id.
    /// </summary>
    /// <param name="id">Entity identifier.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that returns the operation result.</returns>
    public async Task<Auction?> GetAuctionById(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Auctions.FindAsync(new object[] { id }, cancellationToken);
    }

    /// <summary>
    /// Adds auction async.
    /// </summary>
    /// <param name="auction">Auction.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task AddAuctionAsync(Auction auction, CancellationToken cancellationToken)
    {
        await _context.Auctions.AddAsync(auction, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Performs the save auction async operation.
    /// </summary>
    /// <param name="auction">Auction.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task SaveAuctionAsync(Auction auction, CancellationToken cancellationToken)
    {
        _context.Auctions.Update(auction);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
