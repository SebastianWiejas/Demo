using UI.Tests.Helpers;
using UI.Tests.Pages;

namespace UI.Tests.Tests;
public class CartTests : TestsBase
{
        private const string _item1 = "Sauce Labs Backpack";
        private const string _item2 = "Sauce Labs Bike Light";
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
                await inventoryPage.AddItemToCart(_item1);

                var cartPage = new CartPage(Page);
                await cartPage.GoToAsync();

                Assert.True(await cartPage.IsItemInCartAsync(_item1));
        }

        [Fact]
        public async Task AddMultipleItemsToCartTest()
        {
                var inventoryPage = new InventoryPage(Page);
                await inventoryPage.GoToAsync();
                await inventoryPage.AddItemToCart(_item1);
                await inventoryPage.AddItemToCart(_item2);

                var cartPage = new CartPage(Page);
                await cartPage.GoToAsync();

                Assert.True(await cartPage.IsItemInCartAsync(_item1));
                Assert.True(await cartPage.IsItemInCartAsync(_item2));
        }

        [Fact]
        public async Task RemoveItemFromCartTest()
        {
                var inventoryPage = new InventoryPage(Page);
                await inventoryPage.GoToAsync();
                await inventoryPage.AddItemToCart(_item1);
                await inventoryPage.AddItemToCart(_item2);
                await inventoryPage.RemoveItemFromCart(_item2);

                var cartPage = new CartPage(Page);
                await cartPage.GoToAsync();

                Assert.True(await cartPage.IsItemInCartAsync(_item1));
                Assert.False(await cartPage.IsItemInCartAsync(_item2));
        }
}