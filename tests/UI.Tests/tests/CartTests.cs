using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;
using UI.Tests.Helpers;
using UI.Tests.Pages;
namespace UI.Tests.Tests;
public class CartTests : TestsBase
{

        public override async Task InitializeAsync()
        {
                await base.InitializeAsync().ConfigureAwait(false);
                await Page.LoginAsStandardUserAsync();
        }

        [Fact]
        public async Task AddSingleItemToCartTest()
        {
                var inventoryPage = new InventoryPage(Page);
                await inventoryPage.GoToAsync();
                await inventoryPage.AddItemToCart("Sauce Labs Backpack");

                var cartPage = new CartPage(Page);
                await cartPage.GoToAsync();

                Assert.True(await cartPage.IsItemInCartAsync("Sauce Labs Backpack"));
        }

        [Fact]
        public async Task AddMultipleItemsToCartTest()
        {
                var inventoryPage = new InventoryPage(Page);
                await inventoryPage.GoToAsync();
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
                var inventoryPage = new InventoryPage(Page);
                await inventoryPage.GoToAsync();
                await inventoryPage.AddItemToCart("Sauce Labs Backpack");
                await inventoryPage.AddItemToCart("Sauce Labs Bike Light");
                await inventoryPage.RemoveItemFromCart("Sauce Labs Bike Light");

                var cartPage = new CartPage(Page);
                await cartPage.GoToAsync();

                Assert.True(await cartPage.IsItemInCartAsync("Sauce Labs Backpack"));
                Assert.False(await cartPage.IsItemInCartAsync("Sauce Labs Bike Light"));
        }
}