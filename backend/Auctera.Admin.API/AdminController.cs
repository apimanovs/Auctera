using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Auctera.Admin.Domain.Models;
using MediatR;
using Auctera.Admin.Application.Queries;
using Auctera.Admin.Application.Commands;
using Auctera.Shared.Domain.Results;
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
    [Authorize]
    public async Task<ActionResult<IReadOnlyList<AdminDashboardDto>>> GetDashboardData(GetAdminDashboardData getAdminDashboardData, CancellationToken cancellationToken)
    {
        var result = _mediator.Send(getAdminDashboardData, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [Route("accept/{lotId}")]
    [Authorize]
    public async Task<ActionResult> AcceptPendingLot(AcceptLotCommand acceptLotCommand, CancellationToken cancellationToken)
    {
        var result = _mediator.Send(acceptLotCommand, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [Route("reject/{lotId}")]
    [Authorize]
    public async Task<ActionResult> RejectPendingLot(RejectLotCommand rejectLotCommand, CancellationToken cancellationToken)
    {
        var result = _mediator.Send(rejectLotCommand, cancellationToken);

        return Ok(result);
    }
}
