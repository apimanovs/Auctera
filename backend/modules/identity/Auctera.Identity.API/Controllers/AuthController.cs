using Auctera.Identity.Application.Commands;
using Auctera.Identity.Application.Interfaces;
using Auctera.Identity.Application.Models;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Auctera.Identity.Infrastructure.Claims;

namespace Auctera.Identity.API.Controllers;

[ApiController]
[Route("api/auth")]
[EnableRateLimiting("AuthLoginPolicy")]
/// <summary>
/// Represents the auth controller class.
/// </summary>
public sealed class AuthController(IMediator mediator, ICookieFactory cookieFactory) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly ICookieFactory _cookieFactory = cookieFactory;

    [HttpPost("register")]
    [AllowAnonymous]
    [EnableRateLimiting("AuthLoginPolicy")]
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
    [EnableRateLimiting("AuthLoginPolicy")]
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
    [EnableRateLimiting("AuthRefreshPolicy")]
    public async Task<ActionResult<AuthResult>> Refresh(CancellationToken cancellationToken)
    {
        var refreshToken = Request.Cookies["refresh_token"];

        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            return Unauthorized();
        }

        var result = await _mediator.Send(new RefreshTokenCommand(refreshToken), cancellationToken);

        _cookieFactory.SetAccessTokenCookie(Response, result.AccessToken);
        _cookieFactory.SetRefreshTokenCookie(Response, result.RefreshToken);

        return Ok(result);
    }

    [HttpGet("me")]
    [Authorize]
    [EnableRateLimiting("AuthMePolicy")]
    public async Task<ActionResult<AuthResult>> CurrentUser(CancellationToken cancellationToken)
    {
        var user = User.Claims.GetUserId();

        var result = await _mediator.Send(new CurrentUserCommand(user), cancellationToken);

        return Ok(result);
    }
}
