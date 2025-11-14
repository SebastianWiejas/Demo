using Microsoft.Playwright;
namespace UI.Tests.Pages;

public class CheckoutStepOnePage
{
    private readonly IPage _page;

    private ILocator CheckoutContainer => _page.Locator("");
    private ILocator FirstNameInput => CheckoutContainer.Locator("");
    private ILocator LastNameInput => CheckoutContainer.Locator("");
    private ILocator PostalCodeInput => CheckoutContainer.Locator("");
    private ILocator ContinueButton => CheckoutContainer.Locator("");

    public CheckoutStepOnePage(IPage page)
    {
        _page = page;
    }

    public async Task EnterFirstNameAsync(string firstName)
    {
        await CheckoutContainer.Locator(FirstNameInput).FillAsync(firstName);
    }

    public async Task EnterLastNameAsync(string lastName)
    {
        await CheckoutContainer.Locator(LastNameInput).FillAsync(lastName);
    }

    public async Task EnterPostalCodeAsync(string postalCode)
    {
        await CheckoutContainer.Locator(PostalCodeInput).FillAsync(postalCode);
    }

    public async Task ClickContinueAsync()
    {
        await CheckoutContainer.Locator(ContinueButton).ClickAsync();
    }
}