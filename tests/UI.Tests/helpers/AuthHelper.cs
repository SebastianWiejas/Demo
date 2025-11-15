using Microsoft.Playwright;

namespace UI.Tests.Helpers;

public static class AuthHelper
{
    public static async Task LoginAsStandardUserAsync(this IPage page)
    {
        await page.SetCookieAsync("session-username", "standard_user", "www.saucedemo.com", DateTimeOffset.Now.AddHours(10).ToUnixTimeSeconds());
    }
}