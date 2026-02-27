using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Auctera.Shared.Domain.Abstractions;

namespace Auctera.Orders.Domain.Events;
public sealed class OrderExpiderDomainEvent : IDomainEvent
{
    public Guid OrderId { get; }
    public DateTime OccurredAt { get; }

    public OrderExpiderDomainEvent(Guid orderId, DateTime expiredAt)
    {
        OrderId = orderId;
        OccurredAt = expiredAt;
    }
}
