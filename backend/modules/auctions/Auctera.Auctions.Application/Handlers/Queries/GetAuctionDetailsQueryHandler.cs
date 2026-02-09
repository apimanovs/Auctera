using Auctera.Auctions.Application.Models;
using Auctera.Auctions.Application.Queries;
using Auctera.Persistance;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Auctera.Auctions.Application.Handlers.Queries;

public sealed class GetAuctionDetailsQueryHandler
    : IRequestHandler<GetAuctionDetailsQuery, AuctionDetailsDto>
{
    private readonly AucteraDbContext _context;

    public GetAuctionDetailsQueryHandler(AucteraDbContext context)
    {
        _context = context;
    }

    public async Task<AuctionDetailsDto> Handle(
        GetAuctionDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var dto = await _context.Auctions
            .AsNoTracking()
            .Where(a => a.Id == request.AuctionId)
            .Select(a => new AuctionDetailsDto
            {
                AuctionId = a.Id,
                Status = a.Status.ToString(),

                CurrentPrice = a.CurrentPrice.Amount,
                Currency = a.CurrentPrice.Currency,

                StartsAt = a.StartDate,
                EndsAt = a.EndDate,

                LotId = a.Lots.Select(l => l.Id).Single(),
                LotTitle = a.Lots.Select(l => l.Title).Single()
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (dto is null)
        {
            throw new InvalidOperationException("Auction not found");
        }

        return dto;
    }
}
