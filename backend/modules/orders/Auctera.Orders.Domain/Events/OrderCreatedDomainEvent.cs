using Auctera.Orders.Domain.Enums;
using Auctera.Shared.Domain.Abstractions;

namespace Auctera.Orders.Domain.Events;
public class OrderCreatedDomainEvent : IDomainEvent
{
    public Guid AuctionId { get; set; }
    public Guid SellerId { get; set; }
    public Guid BuyerId { get; set; }

    public OrderStatus Status { get; set; }

    public double Price { get; set; }
    public string Currency { get; set; }

    public DateTime PaymentDeadlineUtc { get; set; }
    public DateTime OccurredAt { get; set; }

    public OrderCreatedDomainEvent
    (
        Guid auctionId,
        Guid sellerId,
        Guid buyerId,
        OrderStatus status,
        double price,
        string currency,
        DateTime paymentDeadlineUtc
    )
    {
        AuctionId = auctionId;
        SellerId = sellerId;
        BuyerId = buyerId;
        Status = status;
        Price = price;
        Currency = currency;
        PaymentDeadlineUtc = paymentDeadlineUtc;
        OccurredAt = DateTime.UtcNow;
    }
}
