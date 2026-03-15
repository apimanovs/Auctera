using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using Auctera.Orders.Application.Queries;

namespace Auctera.Orders.API.Controllers;

[ApiController]
[Route("api/orders")]
/// <summary>
/// Represents the orders controller class.
/// </summary>
public sealed class OrdersController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [Authorize]
    /// <summary>
    /// Gets orders for user.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that returns the operation result.</returns>
    public async Task<ActionResult> GetOrdersForUser(CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.Claims.First(c => c.Type == "sub").Value);
        var result = await _mediator.Send(new GetOrdersByUserIdQuery(userId),cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    [Route("details/{orderId:guid}")]
    /// <summary>
    /// Gets order details.
    /// </summary>
    /// <param name="orderId">Identifier of order.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that returns the operation result.</returns>
    public async Task<ActionResult> GetOrderDetails(Guid orderId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetOrderDetailsQuery(orderId),cancellationToken);
        return Ok(result);
    }
}
