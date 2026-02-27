using Auctera.Orders.Application.Interfaces;
using Auctera.Orders.Domain;
using Auctera.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Auctera.Orders.Infrastructure.Repository;

public sealed class OrdersRepository (AucteraDbContext aucteraDbContext) : IOrderRepository
{
    private readonly AucteraDbContext _aucteraDbContext = aucteraDbContext;

    public async Task AddOrderAsync(Order order, CancellationToken cancellationToken)
    {
        _aucteraDbContext.Orders.Add(order);
        await _aucteraDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Order>> GetOrdersByBuyerId(Guid buyerId, CancellationToken cancellationToken)
    {
        return await _aucteraDbContext.Orders.Where(o => o.BuyerId == buyerId).ToListAsync(cancellationToken);
    }
    
    public async Task<IReadOnlyList<Order>> GetOrdersBySellerId(Guid sellerId, CancellationToken cancellationToken)
    {
        return await _aucteraDbContext.Orders.Where(o => o.SellerId == sellerId).ToListAsync(cancellationToken);
    }

    public async Task<Order?> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken)
    {
        return await _aucteraDbContext.Orders.FindAsync(new object[] { orderId }, cancellationToken);
    }

    public async Task UpdateOrderAsync(Order order, CancellationToken cancellationToken)
    {
        _aucteraDbContext.Orders.Update(order);
        await _aucteraDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteOrderAsync(Guid orderId, CancellationToken cancellationToken)
    {
        var order = await GetOrderByIdAsync(orderId, cancellationToken);

        if (order != null)
        {
            _aucteraDbContext.Orders.Remove(order);
            await _aucteraDbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<bool> ExistsForAuctionAsync(Guid auctionId, CancellationToken cancellationToken)
    {
        return await _aucteraDbContext.Orders.AnyAsync(o => o.AuctionId == auctionId, cancellationToken);
    }

    public async Task<Order> GetOrderDetailsById(Guid orderId, CancellationToken cancellationToken)
    {
        var order = await _aucteraDbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);

        if (order == null)
        {
            throw new KeyNotFoundException($"Order with ID {orderId} not found.");
        }

        return order;
    }
}
