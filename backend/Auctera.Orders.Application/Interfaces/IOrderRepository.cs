using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Auctera.Orders.Domain;

namespace Auctera.Orders.Application.Interfaces;
public interface IOrderRepository
{
    Task AddOrderAsync(Order order, CancellationToken cancellationToken);
    Task<Order?> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken);
    Task UpdateOrderAsync(Order order, CancellationToken cancellationToken);
    Task DeleteOrderAsync(Guid orderId, CancellationToken cancellationToken);
    Task<bool> ExistsForAuctionAsync(Guid auctionId, CancellationToken cancellationToken);
}
