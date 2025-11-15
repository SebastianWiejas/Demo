using Microsoft.Playwright;
namespace UI.Tests.Pages;

public class CheckoutStepOnePage
{
    private readonly IPage _page;

    private ILocator CheckoutContainer => _page.Locator("");
    private ILocator FirstNameInput => CheckoutContainer.GetByTestId("firstName");
    private ILocator LastNameInput => CheckoutContainer.GetByTestId("lastName");
    private ILocator PostalCodeInput => CheckoutContainer.GetByTestId("postalCode");
    private ILocator ContinueButton => CheckoutContainer.GetByTestId("continue");

    public CheckoutStepOnePage(IPage page)
    {
        _page = page;
    }

    public async Task EnterFirstNameAsync(string firstName)
    {
        await FirstNameInput.FillAsync(firstName);
    }

    public async Task EnterLastNameAsync(string lastName)
    {
        await LastNameInput.FillAsync(lastName);
    }

    public async Task EnterPostalCodeAsync(string postalCode)
    {
        await PostalCodeInput.FillAsync(postalCode);
    }

    public async Task ClickContinueAsync()
    {
        await ContinueButton.ClickAsync();
    }
}