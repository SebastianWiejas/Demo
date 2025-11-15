using Microsoft.Playwright;

namespace UI.Tests.Pages;

public class InventoryPage : BasePage
{
    private readonly ILocator _inventoryContainer;
    private readonly ILocator _inventoryItem;
    private readonly ILocator _inventoryItemName;
    private readonly ILocator _inventoryItemAddToCartButton;
    private readonly ILocator _inventoryItemRemoveToCartButton;

    public InventoryPage(IPage page) : base(page)
    {
        _inventoryContainer = _page.GetByTestId("inventory-container");
        _inventoryItem = _inventoryContainer.GetByTestId("inventory-item");
        _inventoryItemName = _page.GetByTestId("inventory-item-name");
        _inventoryItemAddToCartButton = _page.Locator("button").Filter(new LocatorFilterOptions { HasTextString = "Add to cart" });
        _inventoryItemRemoveToCartButton = _page.Locator("button").Filter(new LocatorFilterOptions { HasTextString = "Remove" });

    }
    public async Task GoToAsync()
    {
        await _page.GotoAsync("/inventory.html");
    }
    public async Task<bool> IsItemInInventoryAsync(string name)
    {
        return await GetInventoryItemWithNameAsync(name).Result.CountAsync() == 1; //TODO: explain in readme why I use == 1, and alternatives
    }

    public async Task AddItemToCart(string name)
    {
        var item = await GetInventoryItemWithNameAsync(name);
        await ClickAddToCartButton(item);
    }
    public async Task RemoveItemFromCart(string name)
    {
        var item = await GetInventoryItemWithNameAsync(name);
        await ClickRemoveFromCartButton(item);
    }

    private async Task ClickAddToCartButton(ILocator item)
    {
        await item.Locator(_inventoryItemAddToCartButton).ClickAsync();
    }

    private async Task ClickRemoveFromCartButton(ILocator item)
    {
        await item.Locator(_inventoryItemRemoveToCartButton).ClickAsync();
    }

    private async Task<ILocator> GetInventoryItemWithNameAsync(string name)
    {
        var inventoryItemNames = _inventoryItemName
            .Filter(new LocatorFilterOptions { HasTextString = name });


        var inventoryItems = await _inventoryItem.Filter(new LocatorFilterOptions { Has = inventoryItemNames }).AllAsync();
        if (inventoryItems.Count == 0)
        {
            throw new Exception($"There is no inventory item with name '{name}'");
        }
        if (inventoryItems.Count > 1)
        {
            throw new Exception($"There is more than one inventory item with name '{name}'");
        }
        return inventoryItems[0];
    }
}