using Auctera.Bids.Application.Interfaces;
using Auctera.Persistance;
using Auctera.Bids.Domain;

namespace Auctera.Bids.Infrastructure.Repository;

public sealed class BidRepository : IBidRepository
{
    private readonly AucteraDbContext _dbContext;

    public BidRepository(AucteraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddBidAsync(Domain.Bid bid, CancellationToken cancellationToken)
    {
        await _dbContext.Bids.AddAsync(bid, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Domain.Bid?> GetBidById(Guid bidId, CancellationToken cancellationToken)
    {
        return await _dbContext.Bids.FindAsync(new object[] { bidId }, cancellationToken);
    }

    public async Task SaveBidAsync(Domain.Bid bid, CancellationToken cancellationToken)
    {
        _dbContext.Bids.Update(bid);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
