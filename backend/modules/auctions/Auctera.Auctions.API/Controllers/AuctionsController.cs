using Auctera.Auctions.Application.Commands;
using Auctera.Auctions.Application.Models;
using Auctera.Auctions.Application.Queries;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auctera.Auctions.API.Controllers;

[ApiController]
[Route("api/auctions")]
/// <summary>
/// Represents the auctions controller class.
/// </summary>
public sealed class AuctionsController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuctionsController"/> class.
    /// </summary>
    /// <param name="mediator">Mediator.</param>
    public AuctionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{auctionId:guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<IReadOnlyList<AuctionDetailsDto>>> GetAuctionDetails(
        [FromRoute] GetAuctionDetailsQuery getAuctionDetailsQuery,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(getAuctionDetailsQuery, cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IReadOnlyList<AuctionListItemDto>>> GetAuctionList(
        [FromQuery] GetAuctionsListQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [Route("create")]
    [Authorize]
    /// <summary>
    /// Creates auction.
    /// </summary>
    /// <param name="request">Input data for the operation.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that returns the operation result.</returns>
    public async Task<ActionResult> CreateAuction([FromBody] CreateAuctionCommand request, CancellationToken cancellationToken)
    {
        var auctionId = await _mediator.Send(request, cancellationToken);
        return Ok(auctionId);
    }
}
