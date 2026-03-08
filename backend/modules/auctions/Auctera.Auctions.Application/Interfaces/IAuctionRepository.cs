using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Auctera.Auctions.Domain;

namespace Auctera.Auctions.Application.Interfaces;

/// <summary>
/// Represents the i auction repository interface.
/// </summary>
public interface IAuctionRepository
{
    Task<Auction?> GetAuctionById(Guid auctionId, CancellationToken cancellationToken);
    Task AddAuctionAsync(Auction auction, CancellationToken cancellationToken);
    Task SaveAuctionAsync(Auction auction, CancellationToken cancellationToken);
}
