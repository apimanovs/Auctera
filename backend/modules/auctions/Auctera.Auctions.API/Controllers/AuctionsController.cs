using Auctera.Auctions.Application.Commands;
using Auctera.Auctions.Application.Models;
using Auctera.Auctions.Application.Queries;

using MediatR;
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
    public async Task<ActionResult<IReadOnlyList<AuctionDetailsDto>>> GetAuctionDetails(Guid auctionId,CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAuctionDetailsQuery(auctionId), cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AuctionListItemDto>>> GetAuctionList(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAuctionsListQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult> CreateAuction([FromBody] CreateAuctionCommand request, CancellationToken cancellationToken)
    {
        var auctionId = await _mediator.Send(request, cancellationToken);
        return Ok();
    }
}
