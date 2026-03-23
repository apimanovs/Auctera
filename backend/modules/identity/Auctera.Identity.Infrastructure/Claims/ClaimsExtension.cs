using System.Security.Claims;

namespace Auctera.Identity.Infrastructure.Claims;

public static class ClaimsExtension
{
    public static Guid GetUserId(this IEnumerable<Claim> claims)
    {
        var userIdClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
        {
            throw new InvalidOperationException("User ID claim not found.");
        }

        return Guid.Parse(userIdClaim.Value);
    }
}
