using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Auctera.Bids.Application.Models;
using Auctera.Bids.Application.Queries;
using Auctera.Persistance;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Auctera.Bids.Application.Handlers.Queries;

/// <summary>
/// Represents the get bids by auction query handler class.
/// </summary>
public sealed class GetBidsByAuctionQueryHandler : IRequestHandler<GetBidsByAuctionQuery, IReadOnlyList<BidsByAuctionDto>>
{
    private readonly AucteraDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetBidsByAuctionQueryHandler"/> class.
    /// </summary>
    /// <param name="context">Context.</param>
    public GetBidsByAuctionQueryHandler(AucteraDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the operation.
    /// </summary>
    /// <param name="request">Input data for the operation.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that returns the operation result.</returns>
    public async Task<IReadOnlyList<BidsByAuctionDto>> Handle(GetBidsByAuctionQuery request, CancellationToken cancellationToken)
    {
        return await _context.Bids
           .AsNoTracking()
           .Where(b => b.AuctionId == request.AuctionId)
           .OrderByDescending(b => b.PlacedAt)
           .Select(b => new BidsByAuctionDto
           {
               BidId = b.Id,
               UserId = b.BidderId,
               Amount = b.Amount.Amount,
               Currency = b.Amount.Currency,
               Status = b.Status.ToString(),
               PlacedAt = b.PlacedAt
           })
           .ToListAsync(cancellationToken);
    }
}
