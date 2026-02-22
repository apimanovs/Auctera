using Auctera.Auctions.Application.Commands;
using Auctera.Shared.Domain.Enums;
using Auctera.Shared.Domain.Time;
using Auctera.Persistance;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Auctera.Host.BackgroundJobs;

public sealed class AuctionAutoStopBackgroundService(
    IServiceScopeFactory scopeFactory,
    IClock clock,
    ILogger<AuctionAutoStopBackgroundService> logger)
    : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
    private readonly IClock _clock = clock;
    private readonly ILogger<AuctionAutoStopBackgroundService> _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await StopExpiredAuctions(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to auto-stop expired auctions.");
            }

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }

    private async Task StopExpiredAuctions(CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AucteraDbContext>();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        var now = _clock.UtcNow;

        var expiredAuctionIds = await db.Auctions
            .AsNoTracking()
            .Where(a => a.Status == AuctionStatus.Active && a.EndDate != null && a.EndDate <= now)
            .Select(a => a.Id)
            .ToListAsync(ct);

        foreach (var auctionId in expiredAuctionIds)
        {
            try
            {
                await mediator.Send(new StopAuctionCommand(auctionId), ct);
                _logger.LogInformation("Auction {AuctionId} auto-stopped.", auctionId);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Could not auto-stop auction {AuctionId}.", auctionId);
            }
        }
    }
}
