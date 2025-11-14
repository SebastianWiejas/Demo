using Microsoft.Playwright;
using Newtonsoft.Json;

namespace UI.Tests.Pages;

public class CheckoutStepTwoPage
{
    private readonly IPage _page;

    private ILocator CheckoutContainer => _page.Locator("");
    private ILocator ItemList => CheckoutContainer.Locator("");
    private ILocator Item => ItemList.Locator("");
    private ILocator ItemName => Item.Locator("");
    private ILocator ItemDescription => Item.Locator("");
    private ILocator ItemPrice => Item.Locator("");
    private ILocator FinishButton => CheckoutContainer.Locator("");
    private ILocator CancelButton => CheckoutContainer.Locator("");
    private ILocator ItemTotalText => CheckoutContainer.Locator("");
    private ILocator TaxText => CheckoutContainer.Locator("");
    private ILocator TotalText => CheckoutContainer.Locator("");
    private ILocator PaymentInfoText => CheckoutContainer.Locator("");
    private ILocator ShippingInfoText => CheckoutContainer.Locator("");

    public string ItemQuantity { get; private set; }

    public CheckoutStepTwoPage(IPage page)
    {
        _page = page;
    }

    public async Task ClickFinishAsync()
    {
        await CheckoutContainer.Locator(FinishButton).ClickAsync();
    }

    public async Task ClickCancelAsync()
    {
        await CheckoutContainer.Locator(CancelButton).ClickAsync();
    }

    public async Task<string> GetItemTotalAsync()
    {
        return await CheckoutContainer.Locator(ItemTotalText).InnerTextAsync();
    }
    public async Task<string> GetTaxAsync()
    {
        return await CheckoutContainer.Locator(TaxText).InnerTextAsync();
    }
    public async Task<string> GetTotalAsync()
    {
        return await CheckoutContainer.Locator(TotalText).InnerTextAsync();
    }
    public async Task<string> GetPaymentInfoAsync()
    {
        return await CheckoutContainer.Locator(PaymentInfoText).InnerTextAsync();
    }
    public async Task<string> GetShippingInfoAsync()
    {
        return await CheckoutContainer.Locator(ShippingInfoText).InnerTextAsync();
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

    public async Task<ILocator> GetItemByNameAsync(string name)
    {
        var items = await ItemName
            .Filter(new LocatorFilterOptions { HasTextString = name }).AllAsync();

        if (items.Count == 0)
        {
            throw new Exception($"There is no item with name '{name}'");
        }
        if (items.Count > 1)
        {
            throw new Exception($"There is more than one item with name '{name}'");
        }

        return Item.Filter(new LocatorFilterOptions { Has = items[0] }).First;
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