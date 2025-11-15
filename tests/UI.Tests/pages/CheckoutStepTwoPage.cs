using Microsoft.Playwright;

namespace UI.Tests.Pages;

public class CheckoutStepTwoPage
{
    private readonly IPage _page;

    private ILocator CheckoutContainer => _page.GetByTestId("checkout-summary-container");
    private ILocator ItemList => CheckoutContainer.GetByTestId("cart-list");
    private ILocator Item => ItemList.GetByTestId("inventory-item");
    private ILocator ItemName => _page.GetByTestId("inventory-item-name");
    private ILocator ItemDescription => _page.GetByTestId("inventory-item-desc");
    private ILocator ItemPrice => _page.GetByTestId("inventory-item-price");
    private ILocator FinishButton => CheckoutContainer.GetByTestId("finish");
    private ILocator CancelButton => CheckoutContainer.GetByTestId("cancel");
    private ILocator ItemTotalText => CheckoutContainer.GetByTestId("subtotal-label");
    private ILocator TaxText => CheckoutContainer.GetByTestId("tax-label");
    private ILocator TotalText => CheckoutContainer.GetByTestId("total-label");
    private ILocator PaymentInfoText => CheckoutContainer.GetByTestId("payment-info-value");
    private ILocator ShippingInfoText => CheckoutContainer.GetByTestId("shipping-info-value");
    public ILocator ItemQuantity => _page.GetByTestId("item-quantity");

    public CheckoutStepTwoPage(IPage page)
    {
        _page = page;
    }

    public async Task ClickFinishAsync()
    {
        await FinishButton.ClickAsync();
    }

    public async Task ClickCancelAsync()
    {
        await CancelButton.ClickAsync();
    }

    public async Task<string> GetItemTotalAsync()
    {
        return await ItemTotalText.InnerTextAsync();
    }
    public async Task<string> GetTaxAsync()
    {
        return await TaxText.InnerTextAsync();
    }
    public async Task<string> GetTotalAsync()
    {
        return await TotalText.InnerTextAsync();
    }
    public async Task<string> GetPaymentInfoAsync()
    {
        return await PaymentInfoText.InnerTextAsync();
    }
    public async Task<string> GetShippingInfoAsync()
    {
        return await ShippingInfoText.InnerTextAsync();
    }

    public async Task<int> GetNumberOfItems()
    {
        return await Item.CountAsync();
    }

    public async Task<bool> IsItemWithNameInListAsync(string name)
    {
        var items = await ItemName
            .Filter(new LocatorFilterOptions { HasTextString = name }).AllAsync();

        return items.Count > 0;
    }

    private async Task<ILocator> GetItemByNameAsync(string name)
    {
        var items = ItemName
            .Filter(new LocatorFilterOptions { HasTextString = name });

        var parentItems = await Item.Filter(new LocatorFilterOptions { Has = items }).AllAsync();

        if (parentItems.Count == 0)
        {
            throw new Exception($"There is no item with name '{name}'");
        }
        if (parentItems.Count > 1)
        {
            throw new Exception($"There is more than one item with name '{name}'");
        }
        
        return parentItems[0];
    }

    public async Task<string> GetItemDescriptionAsync(string name)
    {
        var item = await GetItemByNameAsync(name);
        return await item.Locator(ItemDescription).InnerTextAsync();
    }

    public async Task<string> GetItemPriceAsync(string name)
    {
        var item = await GetItemByNameAsync(name);
        return await item.Locator(ItemPrice).InnerTextAsync();
    }

    public async Task<int> GetItemQuantityAsync(string name)
    {
        var item = await GetItemByNameAsync(name);
        var quantityText = await item.Locator(ItemQuantity).InnerTextAsync();
        return int.Parse(quantityText);
    }

}