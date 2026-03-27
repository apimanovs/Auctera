using Auctera.Identity.Application.Commands;
using Auctera.Identity.Application.Interfaces;
using Auctera.Identity.Application.Models;

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
    public async Task<ActionResult<AuthResult>> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        _cookieFactory.SetAccessTokenCookie(Response, result.AccessToken);
        _cookieFactory.SetRefreshTokenCookie(Response, result.RefreshToken);

        return Ok(result);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    /// <summary>
    /// Performs the login operation.
    /// </summary>
    /// <param name="command">Input data for the operation.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that returns the operation result.</returns>
    public async Task<ActionResult<AuthResult>> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        _cookieFactory.SetAccessTokenCookie(Response, result.AccessToken);
        _cookieFactory.SetRefreshTokenCookie(Response, result.RefreshToken);

        return Ok(result);
    }

    [HttpPost("logout")]
    public async Task<ActionResult> Logout(CancellationToken cancellationToken)
    {
        var refreshToken = Request.Cookies["refresh_token"];

        await _mediator.Send(new LogoutCommand(refreshToken), cancellationToken);

        _cookieFactory.DeleteAccessTokenCookie(Response);
        _cookieFactory.DeleteRefreshTokenCookie(Response);

        return Ok();
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<AuthResult>> Refresh([FromBody] RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        _cookieFactory.SetAccessTokenCookie(Response, result.AccessToken);
        _cookieFactory.SetRefreshTokenCookie(Response, result.RefreshToken);

        return Ok(result);
    }
}
