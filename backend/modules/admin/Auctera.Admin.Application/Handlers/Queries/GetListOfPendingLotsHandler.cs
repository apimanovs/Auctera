using Auctera.Admin.Application.Queries;
using Auctera.Admin.Domain.Models;
using Auctera.Items.Domain.Enums;
using Auctera.Persistance;
using Auctera.Shared.Domain.Enums;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Auctera.Admin.Application.Handlers.Queries;

public sealed class GetPendingLotsPreviewHandler
    : IRequestHandler<GetListOfPendingLots, IReadOnlyList<PendingLotPreviewDto>>
{
    private readonly AucteraDbContext _context;

    public GetPendingLotsPreviewHandler(AucteraDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<PendingLotPreviewDto>> Handle(GetListOfPendingLots request, CancellationToken cancellationToken)
    {
        var pendingLots = await _context.Lots
            .AsNoTracking()
            .Where(l => l.Status == LotStatus.Pending)
            .Select(l => new PendingLotPreviewDto
            {
                Id = l.Id,
                Title = l.Title,
                Brand = l.Brand,
                PriceAmount = l.Price.Amount,
                Currency = l.Price.Currency,
                Condition = l.Condition,
                Status = l.Status,
                SellerId = l.SellerId,
                MainPhotoKey = l.Media
                    .Where(m => m.Type == "photo")
                    .Select(m => m.Key)
                    .FirstOrDefault()
            })
            .ToListAsync(cancellationToken);

        return pendingLots;
    }
}
