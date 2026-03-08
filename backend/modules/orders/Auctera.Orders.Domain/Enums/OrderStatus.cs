namespace Auctera.Orders.Domain.Enums;

/// <summary>
/// Represents the order status enum.
/// </summary>
public enum OrderStatus
{
    PendingPayment,
    Paid,
    PaymentExpired,
    Cancelled,
    Refunded,
    Failed
}
