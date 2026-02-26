namespace Auctera.Orders.Domain.Enums;

public enum OrderStatus
{
    PendingPayment,
    Paid,
    PaymentExpired,
    Cancelled,
    Refunded
}
