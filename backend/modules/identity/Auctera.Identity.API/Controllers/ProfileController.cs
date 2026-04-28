using Auctera.Identity.Application.Commands;
using Auctera.Identity.Application.Models;
using Auctera.Identity.Application.Queries;
using Auctera.Identity.Infrastructure.Claims;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auctera.Identity.API.Controllers;

[ApiController]
[Route("api/profile")]
public sealed class ProfileController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{username}")]
    [AllowAnonymous]
    public async Task<ActionResult<UserProfileDto>> GetUserProfile(string username, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetUserProfileQuery(username), cancellationToken);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("me/settings")]
    [Authorize]
    public async Task<ActionResult<ProfileSettingsDto>> GetCurrentUserProfileSettings(CancellationToken cancellationToken)
    {
        var userId = User.Claims.GetUserId();
        var result = await _mediator.Send(new GetCurrentUserProfileSettingsQuery(userId), cancellationToken);

        return Ok(result);
    }

    [HttpPut("me/settings")]
    [Authorize]
    public async Task<ActionResult<ProfileSettingsDto>> UpdateCurrentUserProfileSettings(
        [FromBody] UpdateProfileSettingsRequest request,
        CancellationToken cancellationToken)
    {
        var userId = User.Claims.GetUserId();
        var result = await _mediator.Send(new UpdateProfileSettingsCommand(
            userId,
            request.Name,
            request.Username,
            request.City,
            request.Country), cancellationToken);

        return Ok(result);
    }
}

public sealed record UpdateProfileSettingsRequest(
    string Name,
    string Username,
    string? City,
    string? Country
);
