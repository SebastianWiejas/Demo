using System.Diagnostics;
using Microsoft.Playwright;
using UI.Tests.Helpers;
using UI.Tests.Pages;

namespace UI.Tests.Tests;
public class OrderTests : TestsBase
{
        [Fact]
        public async Task OrderItemsTest()
        {
                var item1 = "Sauce Labs Backpack";
                var item2 = "Sauce Labs Bike Light";

                var expectedItem1Price = "$29.99";
                var expectedItem2Price = "$9.99";
                var expectedPaymentInfo = "SauceCard #31337";
                var expectedShippingInfo = "Free Pony Express Delivery!";
                var expectedItemTotal = $"Item total: $39.98";
                var expectedTax = $"Tax: $3.20";
                var expectedTotal = $"Total: $43.18";
                var expectedHeaderText = "Thank you for your order!";

                var firstName = "Seb";
                var lastName = "Sebowy";
                var postalCode = "00000";


                await Page.LoginAsStandardUserAsync();
                var inventoryPage = new InventoryPage(Page);
                await inventoryPage.GoToAsync();
                await inventoryPage.AddItemToCart(item1);
                await inventoryPage.AddItemToCart(item2);

                var cartPage = new CartPage(Page);
                await cartPage.GoToAsync();
                await cartPage.ProceedToCheckoutAsync();

                var checkoutStepOnePage = new CheckoutStepOnePage(Page);
                await checkoutStepOnePage.EnterFirstNameAsync(firstName);
                await checkoutStepOnePage.EnterLastNameAsync(lastName);
                await checkoutStepOnePage.EnterPostalCodeAsync(postalCode);
                await checkoutStepOnePage.ClickContinueAsync();

                var checkoutStepTwoPage = new CheckoutStepTwoPage(Page);

                Assert.Equal(2, await checkoutStepTwoPage.GetNumberOfItems());
                Assert.True(await checkoutStepTwoPage.IsItemWithNameInListAsync(item1));
                Assert.True(await checkoutStepTwoPage.IsItemWithNameInListAsync(item2));
                Assert.Equal(expectedItem1Price, await checkoutStepTwoPage.GetItemPriceAsync(item1));
                Assert.Equal(expectedItem2Price, await checkoutStepTwoPage.GetItemPriceAsync(item2));
                Assert.Equal(expectedPaymentInfo, await checkoutStepTwoPage.GetPaymentInfoAsync());
                Assert.Equal(expectedShippingInfo, await checkoutStepTwoPage.GetShippingInfoAsync());
                Assert.Equal(expectedItemTotal, await checkoutStepTwoPage.GetItemTotalAsync());
                Assert.Equal(expectedTax, await checkoutStepTwoPage.GetTaxAsync());
                Assert.Equal(expectedTotal, await checkoutStepTwoPage.GetTotalAsync());

                await checkoutStepTwoPage.ClickFinishAsync();
                var checkoutCompletePage = new CheckoutCompletePage(Page);
                Assert.Equal(expectedHeaderText, await checkoutCompletePage.GetCompleteHeaderText());
        }
}
