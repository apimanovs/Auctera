using Auctera.Orders.Application.Models;
using Auctera.Orders.Application.Queries;
using Auctera.Orders.Application.Interfaces;

using MediatR;

namespace Auctera.Orders.Application.Handlers.Queries;
public sealed class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, IReadOnlyList<OrderDetailsDto>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderDetailsQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
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
