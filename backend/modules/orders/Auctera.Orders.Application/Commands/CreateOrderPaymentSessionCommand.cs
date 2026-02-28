using MediatR;

namespace Auctera.Orders.Application.Commands;
public sealed record CreateOrderPaymentSessionCommand(Guid OrderId, Guid UserId) : IRequest<CreateOrderPaymentSessionResult>;

public sealed record CreateOrderPaymentSessionResult(string Url);
