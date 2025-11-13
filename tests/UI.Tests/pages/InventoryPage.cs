using Microsoft.Playwright;

namespace UI.Tests.Pages;

public class InventoryPage
{
    private readonly IPage _page;
    private readonly ILocator _inventoryContainer;
    private readonly ILocator _inventoryItem;
    private readonly ILocator _inventoryItemName;
    private readonly string _inventoryItemAddToCartButtonQuery;
    private readonly string _inventoryItemRemoveToCartButtonQuery;

    public InventoryPage(IPage page)
    {
        _page = page;
        _inventoryContainer = _page.Locator("#inventory_container");
        _inventoryItem = _inventoryContainer.Locator(".inventory_item");
        _inventoryItemName = _inventoryItem.Locator(".inventory_item_name");
        _inventoryItemAddToCartButtonQuery = "#add-to-cart-sauce-labs-backpack";
        _inventoryItemRemoveToCartButtonQuery = "#remove-sauce-labs-bike-light";
        
    }

    private async Task<ILocator> GetInventoryItemWithNameAsync(string name)
    {
        var child = await _inventoryItemName
            .Filter(new LocatorFilterOptions { HasTextString = name }).AllAsync();
        
        if (child.Count == 0)
        {
            throw new Exception($"There is no inventory item with name '{name}'");
        }
        if (child.Count > 1)
        {
            throw new Exception($"There is more than one inventory item with name '{name}'");
        }

        return _inventoryItem.Filter(new LocatorFilterOptions { Has = child[0] });
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
        await item.Locator(_inventoryItemAddToCartButtonQuery).ClickAsync();
    }

    private async Task ClickRemoveFromCartButton(ILocator item)
    {
        await item.Locator(_inventoryItemRemoveToCartButtonQuery).ClickAsync();
    }

}