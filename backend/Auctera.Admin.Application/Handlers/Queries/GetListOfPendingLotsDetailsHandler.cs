using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Auctera.Admin.Application.Queries;
using Auctera.Admin.Domain.Models;
using Auctera.Persistance;
using Auctera.Shared.Domain.Enums;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Auctera.Admin.Application.Handlers.Queries;

public sealed class GetListOfPendingLotsDetailsHandler : IRequestHandler<GetListOfPendingLotsDetails, IReadOnlyList<PendingLotPreviewDetailsDto>>
{
    private readonly AucteraDbContext _context;

    public GetListOfPendingLotsDetailsHandler(AucteraDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<PendingLotPreviewDetailsDto>> Handle(GetListOfPendingLotsDetails query, CancellationToken cancellationToken)
    {
        var pendingLotsDetails = _context.Lots
            .AsNoTracking()
            .Where(x => x.Status == LotStatus.Pending)
            .Select(x => new PendingLotPreviewDetailsDto
            {
                Id = x.Id,
                Title = x.Title,
                DescriptionPreview = x.Description,
                SellerId = x.SellerId,
                SellerUsername = _context.Users.AsNoTracking().Where(u => u.Id == x.SellerId).Select(u => u.UserName).ToString(),
                SellerEmail = _context.Users.AsNoTracking().Where(u => u.Id == x.SellerId).Select(u => u.Email).ToString(),
                Brand = x.Brand,
                Category = x.Category,
                Condition = x.Condition,
                StartingPrice = x.Price.Amount,
                Currency = x.Price.Currency,
                Status = x.Status,
                MainImageUrl = x.Media
                    .Where(m => m.Type == "photo")
                    .Select(m => m.Key)
                    .FirstOrDefault()
            }).
            ToListAsync(cancellationToken);

        return (IReadOnlyList<PendingLotPreviewDetailsDto>)pendingLotsDetails;
    }
}
