using Auctera.Items.Application.Commands;
using Microsoft.AspNetCore.Authorization;

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
    [HttpPost("create")]
    [Authorize]
    public async Task<ActionResult<Guid>> CreateLot([FromBody] CreateLotCommand command, CancellationToken cancellationToken)
    {
        var lotId = await _mediator.Send(command, cancellationToken);
        return Ok(lotId);
    }

    [HttpPost]
    [Route("{id}/publish")]
    [Authorize]
    public async Task<ActionResult> PublishLot(Guid lotId, Guid sellerId ,CancellationToken cancellationToken)
    {
        await _mediator.Send(new PublishLotCommand(lotId, sellerId), cancellationToken);
        return Ok();
    }
}
