using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Auctera.Orders.Application.Models;

using MediatR;

namespace Auctera.Orders.Application.Queries;

public sealed record GetOrdersByUserIdQuery (Guid userId) : IRequest<IReadOnlyList<OrderListItemDto>>;

