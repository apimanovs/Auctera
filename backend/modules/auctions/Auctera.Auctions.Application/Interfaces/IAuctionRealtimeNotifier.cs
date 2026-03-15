using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctera.Auctions.Application.Interfaces;

/// <summary>
/// Represents the i auction realtime notifier interface.
/// </summary>
public interface IAuctionRealtimeNotifier
{
    Task BidPlaced(Guid auctionId, Guid bidderId ,decimal amount, string currency);
    Task AuctionStarted(Guid auctionId);
    Task AuctionEnded(Guid auctionId, Guid? winnerId, Guid winningBidId);
}
