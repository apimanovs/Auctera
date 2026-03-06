using Auctera.Orders.Application.Commands;
using Auctera.Orders.Application.Interfaces;
using Auctera.Orders.Domain.Enums;
using Auctera.Orders.Infrastructure.Options;
using Auctera.Shared.Domain.Time;

using MediatR;

using Stripe.Checkout;

namespace Auctera.Orders.Application.Service;

public sealed class PaymentService 
{
    private readonly IOrderRepository _orders;
    private readonly IClock _clock;

    public PaymentService(IOrderRepository orders, IClock clock)
    {
        _orders = orders;
        _clock = clock;
    }

    public async Task HandleExpiredCheckoutSession(Session session, CancellationToken cancellationToken)
    {
        var orderIdRaw = session.Metadata.GetValueOrDefault("orderId") ?? session.ClientReferenceId;

        if (!Guid.TryParse(orderIdRaw, out var orderId))
        {
            throw new InvalidOperationException("Invalid order ID in session metadata.");
        }

        var order = await _orders.GetOrderByIdAsync(orderId, cancellationToken);

        if (order == null)
        {
            throw new KeyNotFoundException($"Order {orderId} not found.");
        }

        if (order.Status != OrderStatus.PendingPayment)
        {
            throw new InvalidOperationException("Order is not pending payment.");
        }

        order.MarkAsExpired(session.ExpiresAt);
        await _orders.UpdateOrderAsync(order, cancellationToken);
    }

    public async Task HandleSuccessfulCheckoutSession(Session session, CancellationToken cancellationToken)
    {
        var orderIdRaw = session.Metadata.GetValueOrDefault("orderId") ?? session.ClientReferenceId;

        if (!Guid.TryParse(orderIdRaw, out var orderId))
        {
            throw new InvalidOperationException("Invalid order ID in session metadata.");
        }

        var order = await _orders.GetOrderByIdAsync(orderId, cancellationToken);

        if (order == null)
        {
            throw new KeyNotFoundException($"Order {orderId} not found.");
        }

        if (order.Status == OrderStatus.Paid)
        {
            return;
        }

        if (order.Status != OrderStatus.PendingPayment)
        {
            throw new InvalidOperationException("Order is not pending payment.");
        }

        if (!string.Equals(order.StripeCheckoutSessionId, session.Id, StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Stripe session does not match order.");
        }

        order.MarkAsPaid(_clock.UtcNow);
        await _orders.UpdateOrderAsync(order, cancellationToken);
    }

    public async Task HandleFailedCheckoutSession(Session session, CancellationToken cancellationToken)
    {
        var orderIdRaw = session.Metadata.GetValueOrDefault("orderId") ?? session.ClientReferenceId;

        if (!Guid.TryParse(orderIdRaw, out var orderId))
        {
            throw new InvalidOperationException("Invalid order ID in session metadata.");
        }

        var order = await _orders.GetOrderByIdAsync(orderId, cancellationToken);

        if (order == null)
        {
            throw new KeyNotFoundException($"Order {orderId} not found.");
        }

        if (order.Status != OrderStatus.PendingPayment)
        {
            throw new InvalidOperationException("Order is not pending payment.");
        }

        if (!string.Equals(order.StripeCheckoutSessionId, session.Id, StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Stripe session does not match order.");
        }

        order.MarkAsFailed(_clock.UtcNow);
        await _orders.UpdateOrderAsync(order, cancellationToken);
    }


}
