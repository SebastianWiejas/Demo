using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;
using UI.Tests.helpers;

namespace UI.Tests.Tests;

[Collection("Sequential")]
public abstract class TestsBase : PageTest, IDisposable
{
    public override async Task InitializeAsync()
    {
        await base.InitializeAsync().ConfigureAwait(false);
    }
    public void Dispose()
    {
        Page?.CloseAsync();
        Context?.CloseAsync();
        Browser?.CloseAsync();
    }
    public override BrowserNewContextOptions ContextOptions()
    {
        return new BrowserNewContextOptions()
        {
            ColorScheme = ColorScheme.Light,
            ViewportSize = new()
            {
                Width = 1920,
                Height = 1080
            },
            BaseURL = "https://" + Configuration.BaseUrl,
        };
    }
}
