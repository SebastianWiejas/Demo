using UI.Tests.Pages;
using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;
namespace UI.Tests;

public class LoginTests : PageTest
{
    
    [Theory]
    [InlineData("standard_user", "secret_sauce", true)]
    [InlineData("performance_glitch_user", "secret_sauce", true)]
    [InlineData("locked_out_user", "secret_sauce", false)]
    [InlineData("standard_user", "wrong_password", false)]
    [InlineData("", "secret_sauce", false)]
    [InlineData("standard_user", "", false)]
    public async Task BasicLoginTest(string username, string password, bool shouldSucceed)
    {
        var browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var context = await browser.NewContextAsync();
        var page = await context.NewPageAsync();

        var loginPage = new LoginPage(page);
        await loginPage.GoToAsync();
        await loginPage.Login(username, password);

        if(shouldSucceed)
        {
            var errorMessages = await loginPage.GetErrorMessage();
            Assert.Empty(errorMessages);
            var inventoryItemLink = page.Locator("#item_0_img_link"); //TODO: Refactor this to InventoryPage
            Assert.True(await inventoryItemLink.IsVisibleAsync());
        }
        else
        {
            var errorMessages = await loginPage.GetErrorMessage();
            Assert.NotEmpty(errorMessages);
        }
    }
}