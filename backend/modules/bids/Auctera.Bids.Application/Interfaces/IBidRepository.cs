using Auctera.Bids.Domain;

namespace Auctera.Bids.Application.Interfaces;

public interface IBidRepository
{
    Task<Bid?> GetBidById(Guid bidId, CancellationToken cancellationToken);
    Task AddBidAsync(Bid bid, CancellationToken cancellationToken);
    Task SaveBidAsync(Bid bid, CancellationToken cancellationToken);
}
