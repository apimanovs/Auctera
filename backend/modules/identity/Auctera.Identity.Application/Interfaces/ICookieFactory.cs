using Microsoft.AspNetCore.Http;

namespace Auctera.Identity.Application.Interfaces;

public interface ICookieFactory
{
    void SetAccessTokenCookie(HttpResponse httpResponse, string token);
    void SetRefreshTokenCookie(HttpResponse response, string token);
    void DeleteAccessTokenCookie(HttpResponse response);
    void DeleteRefreshTokenCookie(HttpResponse response);
}
