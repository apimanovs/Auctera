using Auctera.Shared.Domain.Time;

namespace Auctera.Shared.Infrastructure.Time;
public class SystemClock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}
