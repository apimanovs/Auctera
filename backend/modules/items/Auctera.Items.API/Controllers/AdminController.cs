using Auctera.Items.Application.Commands;
using Auctera.Items.Application.Models;
using Auctera.Items.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auctera.Items.API.Controllers;

[ApiController]
[Route("api/admin")]
public sealed class AdminController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("dashboard")]
    [Authorize]
    public async Task<ActionResult<AdminDashboardDto>> GetDashboardData(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAdminDashboardDataQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("dashbaord")]
    [Authorize]
    public async Task<ActionResult<AdminDashboardDto>> GetDashboardDataLegacy(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAdminDashboardDataQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpPost("accept/{lotId:guid}")]
    [Authorize]
    public async Task<ActionResult<Guid>> AcceptPendingLot([FromRoute] Guid lotId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new AcceptLotCommand(lotId), cancellationToken);
        return Ok(result);
    }

    [HttpPost("reject/{lotId:guid}")]
    [Authorize]
    public async Task<ActionResult<Guid>> RejectPendingLot([FromRoute] Guid lotId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RejectLotCommand(lotId), cancellationToken);
        return Ok(result);
    }
}
