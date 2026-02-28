using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

using Auctera.Orders.Application.Queries;
using Auctera.Orders.Application.Interfaces;
using Auctera.Orders.Domain;
using Auctera.Orders.Application.Models;

namespace Auctera.Orders.Application.Handlers.Queries;

public sealed class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, IReadOnlyList<OrderListItemDto>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrdersByUserIdQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IReadOnlyList<OrderListItemDto>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
    {
        var ordersAsBuyerTask = _orderRepository.GetOrdersByBuyerId(request.userId, cancellationToken);

        var ordersAsSellerTask = _orderRepository.GetOrdersBySellerId(request.userId, cancellationToken);

        await Task.WhenAll(ordersAsBuyerTask, ordersAsSellerTask);

        var allOrders = ordersAsBuyerTask.Result
                   .Concat(ordersAsSellerTask.Result)
                   .GroupBy(o => o.Id)
                   .Select(g => g.First())
                   .OrderByDescending(o => o.PaidAtUtc)
                   .ToList();

        var dto = allOrders.Select(o => new OrderListItemDto
        {
            Id = o.Id,
            AuctionId = o.AuctionId,
            SellerId = o.SellerId,
            BuyerId = o.BuyerId,
            Status = o.Status,
            Price = o.Price,
            Currency = o.Currency,
            PaymentDeadlineUtc = o.PaymentDeadlineUtc,
            PaidAtUtc = o.PaidAtUtc
        }).ToList();

        return dto;
    }
}
