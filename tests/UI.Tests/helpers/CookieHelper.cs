using Microsoft.Playwright;

namespace UI.Tests.Helpers;

public static class CookieHelper
{
    public static async Task SetCookieAsync(this IPage page, string name, string value, string domain, float expires, string path = "/")
    {
        await page.Context.AddCookiesAsync(
        [
            new Cookie
            {
                Name = name,
                Value = value,
                Domain = domain,
                Path = path,
                Expires = expires,
                HttpOnly = false,
                Secure = false,
                SameSite = SameSiteAttribute.Lax
            }
        ]);
    }
}