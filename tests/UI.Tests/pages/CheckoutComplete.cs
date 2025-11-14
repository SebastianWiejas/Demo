using Microsoft.Playwright;
namespace UI.Tests.Pages;
public class CheckoutCompletePage
{
    private readonly IPage _page;
    private ILocator _completeContainer => _page.Locator("");
    private ILocator _completeHeader => _completeContainer.Locator("");
    public CheckoutCompletePage(IPage page)
    {
        _page = page;
    }

    public async Task<string> GetCompleteHeaderText()
    {
        return await _completeHeader.InnerTextAsync();
    }

}