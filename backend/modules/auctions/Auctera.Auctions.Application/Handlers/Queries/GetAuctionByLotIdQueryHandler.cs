using Auctera.Auctions.Application.Models;
using Auctera.Auctions.Application.Queries;
using Auctera.Persistance;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Auctera.Auctions.Application.Handlers.Queries;

public sealed class GetAuctionByLotIdQueryHandler
    : IRequestHandler<GetAuctionByLotIdQuery, AuctionDetailsDto?>
{
    private readonly AucteraDbContext _context;

    public GetAuctionByLotIdQueryHandler(AucteraDbContext context)
    {
        _context = context;
    }

    public Task<AuctionDetailsDto?> Handle(GetAuctionByLotIdQuery request, CancellationToken cancellationToken)
    {
        return _context.Auctions
            .AsNoTracking()
            .Where(a => a.LotId == request.LotId)
            .Select(a => new AuctionDetailsDto
            {
                AuctionId = a.Id,
                Status = a.Status.ToString(),
                CurrentPrice = a.CurrentPrice != null ? a.CurrentPrice.Amount : 0m,
                MinimumBid = a.CurrentPrice != null ? a.CurrentPrice.Amount + 1m : 1m,
                BidCount = a.Bids.Count,
                Currency = a.CurrentPrice != null ? a.CurrentPrice.Currency : null,
                StartsAt = a.StartDate,
                EndsAt = a.EndDate,
                LotId = a.LotId,
                LotTitle = string.Empty
            })
            .SingleOrDefaultAsync(cancellationToken);
    }
}
