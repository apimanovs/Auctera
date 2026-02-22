using Microsoft.AspNetCore.Mvc;
using MediatR;

using Auctera.Bids.Application.Models;
using Auctera.Bids.Application.Queries;
using Auctera.Bids.Application.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;

namespace Auctera.Bids.API.Controllers;

[ApiController]
[Route("api/bids")]
public sealed class BidsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BidsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("{auctionId}")]
    [AllowAnonymous]
    public async Task<ActionResult<IReadOnlyList<BidsByAuctionDto>>> GetBidsByAuction(Guid auctionId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetBidsByAuctionQuery(auctionId), cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [Route("place/{auctionId}")]
    [Authorize]
    [EnableRateLimiting("BidsPolicy")]
    public async Task<IActionResult> PlaceBid(Guid auctionId, PlaceBidCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(new PlaceBidCommand(auctionId, request.BidderId, request.Amount, request.Currency), cancellationToken);

        return Ok();
    }
}
