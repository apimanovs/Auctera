using Auctera.Shared.Domain.Abstractions;

namespace Auctera.Orders.Domain.Events;

public class OrderPaidDomainEvent : IDomainEvent
{
    public Guid OrderId { get; }
    public DateTime OccurredAt { get; }

    public OrderPaidDomainEvent(Guid orderId, DateTime paidAt)
    {
        OrderId = orderId;
        OccurredAt = paidAt;
    }
}
