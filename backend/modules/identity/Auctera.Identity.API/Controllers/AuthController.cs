using Auctera.Identity.Application.Commands;
using Auctera.Identity.Application.Interfaces;

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
public sealed class AuthController(IMediator mediator, ICookieFactory cookieFactory) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly ICookieFactory _cookieFactory = cookieFactory;

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

        _cookieFactory.SetCookie(Response, token);

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

        _cookieFactory.SetCookie(Response, token);

        return Ok(token);
    }

    [HttpPost("logout")]
    public async Task<ActionResult> Logout(CancellationToken cancellationToken)
    {
        _cookieFactory.DeleteCookie(Response);

        return Ok();
    }

    [HttpGet("claims")]
    [Authorize]
    public IActionResult ClaimsDebug()
    {
        return Ok(User.Claims.Select(c => new { c.Type, c.Value }));
    }
}
