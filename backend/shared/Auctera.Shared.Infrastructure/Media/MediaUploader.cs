using Amazon.S3;
using Amazon.S3.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Auctera.Shared.Infrastructure.Media;
/// <summary>
/// Represents the media uploader class.
/// </summary>
public sealed class MediaUploader(IOptions<MediaOptions> options, IAmazonS3 s3) : IMediaUploader
{
    private readonly IAmazonS3 _s3 = s3;
    private readonly MediaOptions _mediaOptions = options.Value;

    /// <summary>
    /// Performs the upload async operation.
    /// </summary>
    /// <param name="stream">Stream.</param>
    /// <param name="fileName">File name.</param>
    /// <param name="contentType">Content type.</param>
    /// <returns>A task that returns the operation result.</returns>
    public async Task<string> UploadAsync(Stream stream, string fileName, string contentType)
    {
        var extension = Path.GetExtension(fileName);
        var key = _mediaOptions.key(Guid.NewGuid(), extension);

        var request = new PutObjectRequest
        {
            BucketName = _mediaOptions.BucketName,
            Key = key,
            InputStream = stream,
            ContentType = contentType,
        };

        request.Headers.CacheControl = "public, max-age=31536000"; // chache for 1 year


        await _s3.PutObjectAsync(request);

        return key;
    }
}
