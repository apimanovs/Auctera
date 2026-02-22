using Auctera.Shared.Infrastructure.Media;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auctera.Items.API.Controllers;

[ApiController]
[Route("api/media")]
public sealed class MediaController(IMediaUploader mediaUploader) : ControllerBase
{
    private readonly IMediaUploader _mediaUploader = mediaUploader;

    [HttpPost("upload")]
    [Authorize]
    [RequestSizeLimit(10_000_000)]
    public async Task<ActionResult<string>> Upload([FromForm] IFormFile file)
    {
        if (file is null || file.Length == 0)
        {
            return BadRequest("File is required.");
        }

        await using var stream = file.OpenReadStream();
        var key = await _mediaUploader.UploadAsync(stream, file.FileName, file.ContentType ?? "application/octet-stream");

        return Ok(new { key });
    }
}
