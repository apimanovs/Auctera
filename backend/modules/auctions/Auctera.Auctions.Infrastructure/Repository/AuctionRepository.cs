using Auctera.Auctions.Application.Interfaces;
using Auctera.Persistance;
using Auctera.Auctions.Domain;

namespace Auctera.Auctions.Infrastructure.Repository;

public class AuctionRepository : IAuctionRepository
{
    private readonly AucteraDbContext _context;

    public AuctionRepository(AucteraDbContext context)
    {
        _context = context;
    }

    public async Task<Auction?> GetAuctionById(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Auctions.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task AddAuctionAsync(Auction auction, CancellationToken cancellationToken)
    {
        await _context.Auctions.AddAsync(auction, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task SaveAuctionAsync(Auction auction, CancellationToken cancellationToken)
    {
        _context.Auctions.Update(auction);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
