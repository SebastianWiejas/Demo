using Microsoft.Playwright;
using UI.Tests.Pages;
namespace UI.Tests.Tests;
public class CartTests : TestsBase
{
    [Fact]
    public async Task AddItemToCartTest()
    {
        var loginPage = new LoginPage(Page);
        await loginPage.GoToAsync();
        await loginPage.Login("standard_user", "secret_sauce");

        var inventoryPage = new InventoryPage(Page);
        await inventoryPage.AddItemToCart("Sauce Labs Backpack");

        var cartPage = new CartPage(Page);
        await cartPage.GoToAsync();

        var cartItem = await cartPage.IsItemInCartAsync("Sauce Labs Backpack");
        Assert.True(cartItem);
    }
}