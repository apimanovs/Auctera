using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctera.Shared.Infrastructure.Media;
/// <summary>
/// Represents the i media uploader interface.
/// </summary>
public interface IMediaUploader
{
    Task<string> UploadAsync(Stream stream, string fileName ,string contentType);
    Task DeleteAsync(string key, CancellationToken cancellationToken);
    Task DeleteManyAsync(IEnumerable<string> keys, CancellationToken cancellationToken);
}
