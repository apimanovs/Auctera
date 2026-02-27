using Auctera.Orders.Domain.Enums;
using Auctera.Shared.Domain.Abstractions;
using Auctera.Orders.Domain.Events;

namespace Auctera.Orders.Domain;

public class Order : AggregateRoot<Guid>
{
    public Guid Id { get; private set; }
    public Guid AuctionId { get; private set; }
    public Guid SellerId { get; private set; }
    public Guid BuyerId { get; private set; }

    public OrderStatus Status { get; private set; }

    public decimal Price { get; private set; }
    public string Currency { get; private set; }

    public DateTime PaymentDeadlineUtc { get; private set; }

    public string? StripeCheckoutSessionId { get; private set; }
    public DateTime? PaidAtUtc { get; private set; }

    private Order(Guid id) : base(id) { }

    public static Order Create
    (
        Guid auctionId,
        Guid sellerId,
        Guid buyerId,
        decimal price,
        string currency,
        DateTime deadlineUtc
    )
    {
        var order = new Order(Guid.NewGuid())
        {
            Id = Guid.NewGuid(),
            AuctionId = auctionId,
            SellerId = sellerId,
            BuyerId = buyerId,
            Price = price,
            Currency = currency,
            PaymentDeadlineUtc = deadlineUtc,
            Status = OrderStatus.PendingPayment
        };

        order.AddDomainEvent(new OrderCreatedDomainEvent(
            auctionId,
            sellerId,
            buyerId,
            OrderStatus.PendingPayment,
            price,
            currency,
            deadlineUtc
        ));

        return order;
    }

    public void MarkAsPaid(DateTime paidAtUtc)
    {
        if (Status != OrderStatus.PendingPayment)
        {
            throw new InvalidOperationException("Order is not payable.");
        }

        if (DateTime.UtcNow > PaymentDeadlineUtc)
        {
            throw new InvalidOperationException("Payment deadline expired.");
        }

        Status = OrderStatus.Paid;
        PaidAtUtc = paidAtUtc;
    }

    public void Expire()
    {
        if (Status != OrderStatus.PendingPayment)
        {
            return;
        }

        Status = OrderStatus.PaymentExpired;
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Paid)
        {
            throw new InvalidOperationException("Paid order cannot be cancelled.");
        }

        Status = OrderStatus.Cancelled;
    }

    public void SetStripeSession(string sessionId)
    {
        StripeCheckoutSessionId = sessionId;
    }
}
