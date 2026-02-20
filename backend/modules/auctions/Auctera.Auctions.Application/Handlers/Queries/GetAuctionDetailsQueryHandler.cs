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

                CurrentPrice = a.CurrentPrice != null
                    ? a.CurrentPrice.Amount
                    : 0m,

                Currency = a.CurrentPrice != null
                    ? a.CurrentPrice.Currency
                    : null,

                StartsAt = a.StartDate,
                EndsAt = a.EndDate,

                LotId = a.LotId
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (dto is null)
        {
            throw new InvalidOperationException("Auction not found");
        }

        return dto;
    }

}
