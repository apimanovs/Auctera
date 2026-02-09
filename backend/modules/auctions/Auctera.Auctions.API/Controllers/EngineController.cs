using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Auctera.Auctions.Application.Commands;

namespace Auctera.Auctions.API.Controllers;

[ApiController]
[Route("api/auction")]
public sealed class EngineController : ControllerBase
{
    private readonly IMediator _mediator;

    public EngineController (IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("start/{auctionId}")]
    public async Task<IActionResult> StartAuction(Guid auctionId, TimeSpan timeSpan)
    {
        await _mediator.Send (new StartAuctionCommand (auctionId, timeSpan));
        return Ok();
    }

    [HttpPost]
    [Route("stop/{auctionId}")]
    public async Task<IActionResult> StopAuction(Guid auctionId)
    {
        await _mediator.Send (new StopAuctionCommand (auctionId));
        return Ok();
    }
}
