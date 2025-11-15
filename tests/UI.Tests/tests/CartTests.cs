using Microsoft.Playwright;
using UI.Tests.Pages;
namespace UI.Tests.Tests;
public class CartTests : TestsBase
{
    [Fact]
    public async Task AddSingleItemToCartTest()
    {
            var loginPage = new LoginPage(Page);
            await loginPage.GoToAsync();
            await loginPage.Login("standard_user", "secret_sauce");

            var inventoryPage = new InventoryPage(Page);
            await inventoryPage.AddItemToCart("Sauce Labs Backpack");

            var cartPage = new CartPage(Page);
            await cartPage.GoToAsync();

            Assert.True(await cartPage.IsItemInCartAsync("Sauce Labs Backpack"));
    }

    [Fact]
    public async Task AddMultipleItemsToCartTest()
    {
            var loginPage = new LoginPage(Page);
            await loginPage.GoToAsync();
            await loginPage.Login("standard_user", "secret_sauce");

            var inventoryPage = new InventoryPage(Page);
            await inventoryPage.AddItemToCart("Sauce Labs Backpack");
            await inventoryPage.AddItemToCart("Sauce Labs Bike Light");

            var cartPage = new CartPage(Page);
            await cartPage.GoToAsync();

            Assert.True(await cartPage.IsItemInCartAsync("Sauce Labs Backpack"));
            Assert.True(await cartPage.IsItemInCartAsync("Sauce Labs Bike Light"));
    }

    [Fact]
    public async Task RemoveItemFromCartTest()
    {
            var loginPage = new LoginPage(Page);
            await loginPage.GoToAsync();
            await loginPage.Login("standard_user", "secret_sauce");

            var inventoryPage = new InventoryPage(Page);
            await inventoryPage.AddItemToCart("Sauce Labs Backpack");
            await inventoryPage.AddItemToCart("Sauce Labs Bike Light");
            await inventoryPage.RemoveItemFromCart("Sauce Labs Bike Light");

            var cartPage = new CartPage(Page);
            await cartPage.GoToAsync();

            Assert.True(await cartPage.IsItemInCartAsync("Sauce Labs Backpack"));
            Assert.False(await cartPage.IsItemInCartAsync("Sauce Labs Bike Light"));
    }
}