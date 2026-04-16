using Auctera.Identity.Application.Models;
using Auctera.Identity.Application.Queries;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;
using MediatR;

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

        return Ok(result);
    }
}
