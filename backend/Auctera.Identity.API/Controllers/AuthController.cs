using Auctera.Identity.Application.Commands;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Auctera.Identity.API.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("register")]
    public async Task<ActionResult<string>> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken)
    {
        var token = await _mediator.Send(command, cancellationToken);
        return Ok(token);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
    {
        var token = await _mediator.Send(command, cancellationToken);
        return Ok(token);
    }
}
