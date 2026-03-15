using Auctera.Orders.Domain.Enums;
using Auctera.Shared.Domain.Abstractions;
using Auctera.Orders.Domain.Events;

namespace Auctera.Orders.Domain;

/// <summary>
/// Represents the order class.
/// </summary>
public class Order : AggregateRoot<Guid>
{
    /// <summary>
    /// Gets or sets the id used by this type.
    /// </summary>
    public Guid Id { get; private set; }
    /// <summary>
    /// Gets or sets the auction id used by this type.
    /// </summary>
    public Guid AuctionId { get; private set; }
    /// <summary>
    /// Gets or sets the seller id used by this type.
    /// </summary>
    public Guid SellerId { get; private set; }
    /// <summary>
    /// Gets or sets the buyer id used by this type.
    /// </summary>
    public Guid BuyerId { get; private set; }

    /// <summary>
    /// Gets or sets the status used by this type.
    /// </summary>
    public OrderStatus Status { get; private set; }

    /// <summary>
    /// Gets or sets the price used by this type.
    /// </summary>
    public decimal Price { get; private set; }
    /// <summary>
    /// Gets or sets the currency used by this type.
    /// </summary>
    public string Currency { get; private set; }

    /// <summary>
    /// Gets or sets the payment deadline utc used by this type.
    /// </summary>
    public DateTime PaymentDeadlineUtc { get; private set; }

    /// <summary>
    /// Gets or sets the stripe checkout session id used by this type.
    /// </summary>
    public string? StripeCheckoutSessionId { get; private set; }
    /// <summary>
    /// Gets or sets the paid at utc used by this type.
    /// </summary>
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

    /// <summary>
    /// Marks as paid.
    /// </summary>
    /// <param name="paidAtUtc">Date and time for the operation.</param>
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

    /// <summary>
    /// Marks as failed.
    /// </summary>
    /// <param name="paidAtUtc">Date and time for the operation.</param>
    public void MarkAsFailed(DateTime paidAtUtc)
    {
        if (Status != OrderStatus.PendingPayment)
        {
            throw new InvalidOperationException("Order is not payable.");
        }

        if (DateTime.UtcNow > PaymentDeadlineUtc)
        {
            throw new InvalidOperationException("Payment deadline expired.");
        }

        Status = OrderStatus.Failed;
        PaidAtUtc = paidAtUtc;
    }

    /// <summary>
    /// Marks as expired.
    /// </summary>
    /// <param name="expiredAtUtc">Expired at utc.</param>
    public void MarkAsExpired(DateTime expiredAtUtc)
    {
        if (Status != OrderStatus.PendingPayment)
        {
            return;
        }

        Status = OrderStatus.PaymentExpired;
        AddDomainEvent(new OrderExpiderDomainEvent(Id,expiredAtUtc));
    }

    /// <summary>
    /// Cancels the operation.
    /// </summary>
    public void Cancel()
    {
        if (Status == OrderStatus.Paid)
        {
            throw new InvalidOperationException("Paid order cannot be cancelled.");
        }

        Status = OrderStatus.Cancelled;
    }

    /// <summary>
    /// Sets stripe session.
    /// </summary>
    /// <param name="sessionId">Identifier of session.</param>
    public void SetStripeSession(string sessionId)
    {
        StripeCheckoutSessionId = sessionId;
    }
}
