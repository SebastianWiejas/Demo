using Microsoft.Playwright;

namespace UI.Tests.Pages;

public class BasePage
{
    protected readonly IPage _page;

    public BasePage(IPage page)
    {
        var playwright = Playwright.CreateAsync().GetAwaiter().GetResult();
        playwright.Selectors.SetTestIdAttribute("data-test");
        _page = page;
    }
}
