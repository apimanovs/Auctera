using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

using Auctera.Auctions.Application.Models;
using Auctera.Auctions.Application.Queries;
using Auctera.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Auctera.Auctions.Application.Handlers.Queries;
public sealed class GetCurrentPriceQueryHandler : IRequestHandler<GetCurrentPriceQuery, CurrentPriceDto>
{
    private readonly AucteraDbContext _context;

    public GetCurrentPriceQueryHandler(AucteraDbContext context)
    {
        _context = context;
    }

    public Task<CurrentPriceDto> Handle(GetCurrentPriceQuery request, CancellationToken cancellationToken)
    {
        var dto = _context.Auctions
            .Where(a => a.Id == request.AuctionId)
            .Select(a => new CurrentPriceDto
            {
                AuctionId = a.Id,
                Price = a.CurrentPrice.Amount,
                Currency = a.CurrentPrice.Currency,
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (dto is null)
        {
            throw new InvalidOperationException("Auction not found");
        }

        return dto;
    }
}
