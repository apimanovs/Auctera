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
    public string key(Guid guid, string extenstion) { return $"lots/{guid}{extenstion}"; }

    /// <summary>
    /// Gets or sets the bucket name used by this type.
    /// </summary>
    public string BucketName { get; set; } = default!;
    /// <summary>
    /// Gets or sets the public url used by this type.
    /// </summary>
    public string PublicUrl { get; set; } = default!;
    /// <summary>
    /// Gets or sets the access url used by this type.
    /// </summary>
    public string AccessUrl { get; set; } = default!;
}
