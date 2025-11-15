using Microsoft.Playwright;
namespace UI.Tests.Pages;

[Collection("Sequential")]
public class CheckoutCompletePage
{
    private readonly IPage _page;
    private ILocator _completeContainer => _page.GetByTestId("checkout-complete-container");
    private ILocator _completeHeader => _completeContainer.GetByTestId("complete-header");
    public CheckoutCompletePage(IPage page)
    {
        _page = page;
    }

    public async Task<string> GetCompleteHeaderText()
    {
        return await _completeHeader.InnerTextAsync();
    }

}