using Auctera.Orders.Application.Models;

using MediatR;

namespace Auctera.Orders.Application.Queries;

public sealed record GetOrderDetailsQuery(Guid orderId) : IRequest<IReadOnlyList<OrderDetailsDto>>;
