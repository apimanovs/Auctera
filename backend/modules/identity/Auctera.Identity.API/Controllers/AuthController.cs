using Auctera.Identity.Application.Commands;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Auctera.Identity.API.Controllers;

[ApiController]
[Route("api/auth")]
[EnableRateLimiting("AuthPolicy")]
/// <summary>
/// Represents the auth controller class.
/// </summary>
public sealed class AuthController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("register")]
    [AllowAnonymous]
    /// <summary>
    /// Performs the register operation.
    /// </summary>
    /// <param name="command">Input data for the operation.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that returns the operation result.</returns>
    public async Task<ActionResult<string>> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken)
    {
        var token = await _mediator.Send(command, cancellationToken);
        return Ok(token);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    /// <summary>
    /// Performs the login operation.
    /// </summary>
    /// <param name="command">Input data for the operation.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that returns the operation result.</returns>
    public async Task<ActionResult<string>> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
    {
        var token = await _mediator.Send(command, cancellationToken);
        return Ok(token);
    }
}
