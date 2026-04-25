namespace Auctera.Shared.Infrastructure.Media;

/// <summary>
/// Represents the media options class.
/// </summary>
public sealed class MediaOptions
{
    public const string SectionName = "R2";

    /// <summary>
    /// Performs the key operation.
    /// </summary>
    /// <param name="guid">Identifier of gu.</param>
    /// <param name="extenstion">Extenstion.</param>
    /// <returns>The operation result.</returns>
    public string BuildKey(Guid guid, string extension)
    {
        return $"{guid}{extension}";
    }
    /// <summary>
    /// Gets or sets the bucket name used by this type.
    /// </summary>
    public string BucketName { get; set; } = default!;
    /// <summary>
    /// Gets or sets the public url used by this type.
    /// </summary>
    public string PublicBaseUrl { get; set; } = default!;
    /// <summary>
    /// Gets or sets the access url used by this type.
    /// </summary>
    public string AccessKeyId { get; set; } = default!;
    public string SecretAccessKey { get; set; } = default!;
    public string AccountId { get; set; } = default!;
    public string ServiceUrl { get; set; } = default!;
}
