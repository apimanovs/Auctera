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
public sealed class OrdersController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [Route("{userId}/orders")]
    [Authorize]
    public async Task<ActionResult> GetOrdersForUser(GetOrdersByUserIdQuery query ,CancellationToken cancellationToken)
    {
        await _mediator.Send(query, cancellationToken);
        return Ok();
    }

    [HttpGet]
    [Route("{orderId:guid}")]
    [Authorize]
    public async Task<ActionResult> GetOrderDetails(GetOrderDetailsQuery query, CancellationToken cancellationToken)
    {
        await _mediator.Send(query, cancellationToken);
        return Ok();
    }
}
