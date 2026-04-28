namespace Auctera.Identity.Application.Models;

public sealed class UserProfileDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? City { get; set; }
    public string? Country { get; set; }

    public UserProfileStatsDto Stats { get; set; } = new();

    public List<UserProfileListingDto> ActiveListings { get; set; } = [];
    public List<UserProfileListingDto> SoldListings { get; set; } = [];
}
