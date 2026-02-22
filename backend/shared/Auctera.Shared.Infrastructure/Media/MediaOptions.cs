namespace Auctera.Shared.Infrastructure.Media;

public sealed class MediaOptions
{
    public const string SectionName = "R2";

    public string key(Guid guid, string extenstion) { return $"lots/{guid}{extenstion}"; }

    public string BucketName { get; set; } = default!;
    public string PublicUrl { get; set; } = default!;
    public string AccessUrl { get; set; } = default!;
}
