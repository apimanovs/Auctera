using Auctera.Items.Application.Interfaces;
using Auctera.Items.Application.Models;
using Auctera.Items.Application.Queries;
using Auctera.Shared.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Auctera.Items.Application.Handlers.Queries;

public sealed class GetAdminDashboardDataQueryHandler(ILotRepository lotRepository)
    : IRequestHandler<GetAdminDashboardDataQuery, AdminDashboardDto>
{
    private readonly ILotRepository _lotRepository = lotRepository;

    public async Task<AdminDashboardDto> Handle(GetAdminDashboardDataQuery request, CancellationToken cancellationToken)
    {
        var query = _lotRepository.GetQueryable();

        var draftLots = await query.CountAsync(x => x.Status == LotStatus.Draft, cancellationToken);
        var publishedLots = await query.CountAsync(x => x.Status == LotStatus.Published, cancellationToken);
        var listedLots = await query.CountAsync(x => x.Status == LotStatus.Listed, cancellationToken);
        var soldLots = await query.CountAsync(x => x.Status == LotStatus.Sold, cancellationToken);
        var removedLots = await query.CountAsync(x => x.Status == LotStatus.Removed, cancellationToken);

        return new AdminDashboardDto
        {
            DraftLots = draftLots,
            PublishedLots = publishedLots,
            ListedLots = listedLots,
            SoldLots = soldLots,
            RemovedLots = removedLots,
            TotalLots = draftLots + publishedLots + listedLots + soldLots + removedLots,
        };
    }
}
