using Microsoft.AspNetCore.Mvc;
using Auctera.Admin.Domain.Models;
using MediatR;
using Auctera.Admin.Application.Queries;
using Auctera.Admin.Application.Commands;
using Microsoft.AspNetCore.Authorization;

namespace Auctera.Admin.API;

[ApiController]
[Route("api/admin")]
public sealed class AdminController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminController (IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("dashbaord")]
    public async Task<ActionResult<IReadOnlyList<AdminDashboardDto>>> GetDashboardData(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAdminDashboardData(), cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [Route("accept/{lotId}")]
    public async Task<ActionResult> AcceptPendingLot(AcceptLotCommand acceptLotCommand, CancellationToken cancellationToken)
    {
        var result = _mediator.Send(acceptLotCommand, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [Route("reject/{lotId}")]
    public async Task<ActionResult> RejectPendingLot(RejectLotCommand rejectLotCommand, CancellationToken cancellationToken)
    {
        var result = _mediator.Send(rejectLotCommand, cancellationToken);

        return Ok(result);
    }
}
