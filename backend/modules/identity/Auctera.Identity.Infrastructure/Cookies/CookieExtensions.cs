using Microsoft.AspNetCore.Http;
using Auctera.Identity.Application.Interfaces;

namespace Auctera.Identity.Infrastructure.Cookies;

public class CookieFactory : ICookieFactory
{
    public void SetCookie(HttpResponse response, string token)
    {
        response.Cookies.Append("token", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Expires = DateTimeOffset.UtcNow.AddDays(7),
            Path = "/"
        });
    }

    public void DeleteCookie(HttpResponse response)
    {
        response.Cookies.Delete("token", new CookieOptions
        {
            Path = "/"
        });
    }
}
