using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Auctera.Items.Application.Commands;
using Auctera.Shared.Domain.ValueObjects;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Auctera.Items.API.Controllers;

[ApiController]
[Route("api/items")]
public sealed class ItemController : ControllerBase
{
    private readonly IMediator _mediator;

    public ItemController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult> CreateLot(
        Guid sellerId,
        Guid auctionId,
        decimal price,
        string title,
        string description,
        Money money)
    {
       var lot = await _mediator.Send(new CreateLotCommand (sellerId,auctionId,title, description, money));

        return Ok(lot);
    }
}
