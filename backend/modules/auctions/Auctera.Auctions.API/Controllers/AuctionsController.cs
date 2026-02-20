using Auctera.Auctions.Application.Commands;
using Auctera.Auctions.Application.Models;
using Auctera.Auctions.Application.Queries;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auctera.Auctions.API.Controllers;

[ApiController]
[Route("api/auctions")]
public sealed class AuctionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuctionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{auctionId:guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<IReadOnlyList<AuctionDetailsDto>>> GetAuctionDetails([FromBody] GetAuctionDetailsQuery getAuctionDetailsQuery, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(getAuctionDetailsQuery, cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IReadOnlyList<AuctionListItemDto>>> GetAuctionList([FromBody] GetAuctionsListQuery getAuctionsListQuery, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(getAuctionsListQuery, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [Route("create")]
    [Authorize]
    public async Task<ActionResult> CreateAuction([FromBody] CreateAuctionCommand request, CancellationToken cancellationToken)
    {
        var auctionId = await _mediator.Send(request, cancellationToken);
        return Ok();
    }
}
