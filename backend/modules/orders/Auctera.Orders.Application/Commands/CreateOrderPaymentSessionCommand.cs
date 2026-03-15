using MediatR;

namespace Auctera.Orders.Application.Commands;

/// <summary>
/// Represents the create order payment session command record.
/// </summary>
public sealed record CreateOrderPaymentSessionCommand(Guid OrderId, Guid UserId) : IRequest<CreateOrderPaymentSessionResult>;

/// <summary>
/// Represents the create order payment session result record.
/// </summary>
public sealed record CreateOrderPaymentSessionResult(string Url);
