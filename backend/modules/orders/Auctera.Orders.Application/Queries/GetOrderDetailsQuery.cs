using Auctera.Orders.Application.Models;

using MediatR;

namespace Auctera.Orders.Application.Queries;

/// <summary>
/// Represents the get order details query record.
/// </summary>
public sealed record GetOrderDetailsQuery(Guid orderId) : IRequest<IReadOnlyList<OrderDetailsDto>>;
