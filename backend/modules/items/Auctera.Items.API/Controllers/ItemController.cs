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

    [HttpPost("create")]
    [Authorize]
    public async Task<ActionResult<Guid>> CreateLot([FromBody] CreateLotCommand command, CancellationToken cancellationToken)
    {
        var lotId = await _mediator.Send(command, cancellationToken);
        return Ok(lotId);
    }

    [HttpPost]
    [Route("{lotId}/publish")]
    [Authorize]
    public async Task<ActionResult> PublishLot(Guid lotId,CancellationToken cancellationToken)
    {
        var sellerId = Guid.Parse(User.Claims.First(c => c.Type == "sub").Value);

        await _mediator.Send(new PublishLotCommand(lotId, sellerId), cancellationToken);
        return Ok();
    }
}
