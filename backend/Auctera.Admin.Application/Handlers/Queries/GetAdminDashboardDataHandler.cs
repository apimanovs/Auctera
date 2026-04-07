using Auctera.Admin.Application.Queries;
using Auctera.Admin.Domain.Models;
using Auctera.Persistance;
using Auctera.Shared.Domain.Enums;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Auctera.Admin.Application.Handlers.Queries;

public sealed class GetAdminDashboardDataHandler : IRequestHandler<GetAdminDashboardData, AdminDashboardDto>
{
    private readonly AucteraDbContext _context;

    public GetAdminDashboardDataHandler(AucteraDbContext aucteraDbContext)
    {
        _context = aucteraDbContext;
    }

    public async Task<AdminDashboardDto> Handle(GetAdminDashboardData query, CancellationToken cancellationToken)
    {
        var activeUsersCount = _context.Users.Count();
        var activeAuctionsCount = _context.Auctions.AsNoTracking().Where(a => a.Status == Shared.Domain.Enums.AuctionStatus.Active).Count();
        var pendingLotsCount = _context.Lots.AsNoTracking().Where(a => a.Status == LotStatus.Pending).Count();

        return new AdminDashboardDto { UsersCount = activeAuctionsCount, ActiveAuctionsCount = activeAuctionsCount, PendingLotsCount = pendingLotsCount };
    }
}
