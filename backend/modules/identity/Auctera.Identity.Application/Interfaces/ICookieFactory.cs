using Microsoft.AspNetCore.Http;

namespace Auctera.Identity.Application.Interfaces;
public interface ICookieFactory
{
    void SetCookie(HttpResponse httpResponse, string token);
    void DeleteCookie(HttpResponse httpResponse);
}
