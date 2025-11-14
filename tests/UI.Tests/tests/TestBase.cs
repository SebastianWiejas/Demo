using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;

namespace UI.Tests.Tests;

public abstract class TestsBase : PageTest, IDisposable
{
    public void Dispose()
    {
        Page?.CloseAsync();
        Context?.CloseAsync();
        Browser?.CloseAsync();
    }

}
