using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Auctera.Auctions.Application.Models;
using Auctera.Auctions.Application.Queries;
using Auctera.Persistance;
using Auctera.Shared.Domain.Enums;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Auctera.Auctions.Application.Handlers.Queries;

public sealed class GetAuctionsListQueryHandler : IRequestHandler<GetAuctionsListQuery, IReadOnlyList<AuctionListItemDto>>
{
    private readonly AucteraDbContext _context;

    public GetAuctionsListQueryHandler(AucteraDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<AuctionListItemDto>> Handle(
        GetAuctionsListQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _context.Auctions
            .AsNoTracking()
            .OrderBy(a => a.EndDate)
            .Select(a => new AuctionListItemDto
            {
                AuctionId = a.Id,
                Status = a.Status.ToString(),
                CurrentPrice = a.CurrentPrice.Amount,
                Currency = a.CurrentPrice.Currency,
                EndDate = a.EndDate,
                LotId = a.LotId,
            })
            .ToListAsync(cancellationToken);
    }
}
