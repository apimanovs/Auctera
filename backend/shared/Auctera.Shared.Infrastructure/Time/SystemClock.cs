using Auctera.Shared.Domain.Time;

namespace Auctera.Shared.Infrastructure.Time;
/// <summary>
/// Represents the system clock class.
/// </summary>
public class SystemClock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}
