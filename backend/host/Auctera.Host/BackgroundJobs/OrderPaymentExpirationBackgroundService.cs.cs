using Auctera.Orders.Domain.Enums;
using Auctera.Persistance;
using Auctera.Shared.Domain.Time;
using Auctera.Shared.Infrastructure.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Auctera.Host.BackgroundJobs;

public sealed class OrderPaymentExpirationBackgroundService(
    IServiceScopeFactory scopeFactory,
    IClock clock,
    ILogger<OrderPaymentExpirationBackgroundService> logger)
    : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
    private readonly IClock _clock = clock;
    private readonly ILogger<OrderPaymentExpirationBackgroundService> _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await ExpireOrders(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to expire overdue orders.");
            }

            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        }
    }

    private async Task ExpireOrders(CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AucteraDbContext>();
        var domainEventDispatcher = scope.ServiceProvider.GetRequiredService<IDomainEventDispatcher>();

        var now = _clock.UtcNow;

        var pendingOrders = await db.Orders
            .Where(x => x.Status == OrderStatus.PendingPayment && x.PaymentDeadlineUtc < now)
            .ToListAsync(ct);

        foreach (var order in pendingOrders)
        {
            order.MarkAsExpired(now);
            await domainEventDispatcher.DispatchAsync(order.DomainEvents, ct);
            order.ClearDomainEvents();
        }

        if (pendingOrders.Count > 0)
        {
            await db.SaveChangesAsync(ct);
            _logger.LogInformation("Expired {Count} overdue orders.", pendingOrders.Count);
        }
    }
}
