using Auctera.Items.Application.Commands;
using Microsoft.AspNetCore.Authorization;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Auctera.Items.Application.Queries;

namespace Auctera.Items.API.Controllers;

[ApiController]
[Route("api/items")]
/// <summary>
/// Represents the item controller class.
/// </summary>
public sealed class ItemController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemController"/> class.
    /// </summary>
    /// <param name="mediator">Mediator.</param>
    public ItemController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    [Authorize]
    /// <summary>
    /// Creates lot.
    /// </summary>
    /// <param name="command">Input data for the operation.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that returns the operation result.</returns>
    public async Task<ActionResult<Guid>> CreateLot([FromBody] CreateLotCommand command, CancellationToken cancellationToken)
    {
        var lotId = await _mediator.Send(command, cancellationToken);
        return Ok(lotId);
    }

    [HttpPatch]
    [Route("{lotId}/update")]
    [Authorize]
    /// <summary>
    /// Updates lot.
    /// </summary>
    /// <param name="lotId">Identifier of lot.</param>
    /// <param name="command">Input data for the operation.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that returns the operation result.</returns>
    public async Task<ActionResult> UpdateLot(Guid lotId, [FromBody] EditLotCommand command, CancellationToken cancellationToken)
    {
        if (lotId != command.id)
        {
            return BadRequest("Lot ID in the URL does not match Lot ID in the body.");
        }

        await _mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpDelete]
    [Route("{lotId}/delete")]
    [Authorize]
    /// <summary>
    /// Deletes lot.
    /// </summary>
    /// <param name="lotId">Identifier of lot.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that returns the operation result.</returns>
    public async Task<ActionResult> DeleteLot(Guid lotId, CancellationToken cancellationToken)
    {
        var sellerId = Guid.Parse(User.Claims.First(c => c.Type == "sub").Value);

        await _mediator.Send(new DeleteLotCommand(lotId, sellerId), cancellationToken);
        return Ok();
    }

    [HttpPost]
    [Route("{lotId}/publish")]
    [Authorize]
    /// <summary>
    /// Publishes lot.
    /// </summary>
    /// <param name="lotId">Identifier of lot.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that returns the operation result.</returns>
    public async Task<ActionResult> PublishLot(Guid lotId, CancellationToken cancellationToken)
    {
        var sellerId = Guid.Parse(User.Claims.First(c => c.Type == "sub").Value);

        await _mediator.Send(new PublishLotCommand(lotId, sellerId), cancellationToken);
        return Ok();
    }

    [HttpGet]
    [Route("{lotId:guid}")]
    /// <summary>
    /// Gets lot.
    /// </summary>
    /// <param name="lotId">Identifier of lot.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that returns the operation result.</returns>
    public async Task<ActionResult> GetLot(Guid lotId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetLotQuery(lotId), cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    /// <summary>
    /// Gets lots filtered list.
    /// </summary>
    /// <param name="query">Input data for the operation.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that returns the operation result.</returns>
    public async Task<ActionResult> GetLotsFilteredList([FromQuery] GetLotsListQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }
}
