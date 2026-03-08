using Auctera.Orders.Application.Models;
using Auctera.Orders.Application.Queries;
using Auctera.Orders.Application.Interfaces;

using MediatR;

namespace Auctera.Orders.Application.Handlers.Queries;
/// <summary>
/// Represents the get order details query handler class.
/// </summary>
public sealed class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, IReadOnlyList<OrderDetailsDto>>
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
    public async Task<IReadOnlyList<OrderDetailsDto>> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetOrderByIdAsync(request.orderId, cancellationToken);

        var dto = orders.Select(o => new OrderDetailsDto
        {
            Id = o.Id,
            AuctionId = o.AuctionId,
            SellerId = o.SellerId,
            BuyerId = o.BuyerId,
            Status = o.Status,
            Price = o.Price,
            Currency = o.Currency,
            PaymentDeadlineUtc = o.PaymentDeadlineUtc,
            StripeCheckoutSessionId = o.StripeCheckoutSessionId,
            PaidAtUtc = o.PaidAtUtc
        }).ToList();

        return dto;
    }
}
