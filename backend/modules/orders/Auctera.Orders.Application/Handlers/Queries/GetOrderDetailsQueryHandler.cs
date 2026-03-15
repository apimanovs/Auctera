using Auctera.Orders.Application.Models;
using Auctera.Orders.Application.Queries;
using Auctera.Orders.Application.Interfaces;

using System.Linq;

using MediatR;

namespace Auctera.Orders.Application.Handlers.Queries;
/// <summary>
/// Represents the get order details query handler class.
/// </summary>
public sealed class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, OrderDetailsDto>
{
    private readonly IOrderRepository _orderRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetOrderDetailsQueryHandler"/> class.
    /// </summary>
    /// <param name="orderRepository">Order repository.</param>
    public GetOrderDetailsQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    /// <summary>
    /// Handles the operation.
    /// </summary>
    /// <param name="request">Input data for the operation.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that returns the operation result.</returns>
    public async Task<OrderDetailsDto> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByIdAsync(request.orderId, cancellationToken);

        var dto = new OrderDetailsDto
        {
            Id = order.Id,
            AuctionId = order.AuctionId,
            SellerId = order.SellerId,
            BuyerId = order.BuyerId,
            Status = order.Status,
            Price = order.Price,
            Currency = order.Currency,
            PaymentDeadlineUtc = order.PaymentDeadlineUtc,
            StripeCheckoutSessionId = order.StripeCheckoutSessionId,
            PaidAtUtc = order.PaidAtUtc
        };

        return dto;
    }
}
