namespace Auctera.Items.Application.Models;

public sealed class AdminDashboardDto
{
    public int DraftLots { get; init; }
    public int PublishedLots { get; init; }
    public int ListedLots { get; init; }
    public int SoldLots { get; init; }
    public int RemovedLots { get; init; }
    public int TotalLots { get; init; }
}
