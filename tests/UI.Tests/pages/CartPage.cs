using Microsoft.Playwright;
namespace UI.Tests.Pages;

public class CartPage
{
    private readonly IPage _page;

    private ILocator CartContainer => _page.Locator("");
    private ILocator CartItem => CartContainer.Locator("");
    private ILocator CartItemName => CartItem.Locator("");
    private ILocator CartItemDescription => CartItem.Locator("");
    private ILocator CartItemPrice => CartItem.Locator("");
    private ILocator CartItemQuantity => CartItem.Locator("");
    private ILocator CheckoutButton => CartContainer.Locator("");
    private ILocator RemoveButton => CartItem.Locator("");

    public CartPage(IPage page)
    {
        _page = page;
    }

    public async Task GoToAsync()
    {
        await _page.GotoAsync("https://www.saucedemo.com/cart.html");
    }

    public async Task<int> GetNumberOfItemsInCartAsync()
    {
        return await CartItem.CountAsync();
    }

    private async Task<ILocator> GetCartItemByNameAsync(string name)
    {
        var items = await CartItemName
            .Filter(new LocatorFilterOptions { HasTextString = name }).AllAsync();

        if (items.Count == 0)
        {
            throw new Exception($"There is no cart item with name '{name}'");
        }
        if (items.Count > 1)
        {
            throw new Exception($"There is more than one cart item with name '{name}'");
        }

        return CartItem.Filter(new LocatorFilterOptions { Has = items[0] }).First;
    }

    public async Task RemoveItemFromCartAsync(string name)
    {
        var item = await GetCartItemByNameAsync(name);
        await item.Locator(RemoveButton).ClickAsync();
    }

    public async Task ProceedToCheckoutAsync()
    {
        await CartContainer.Locator(CheckoutButton).ClickAsync();
    }

    public async Task<bool> IsItemInCartAsync(string name)
    {
        return await GetCartItemByNameAsync(name).Result.CountAsync() == 1; //TODO: explain in readme why I use == 1, and alternatives
    }

    public async Task<string> GetItemDescriptionAsync(string name)
    {
        var item = await GetCartItemByNameAsync(name);
        return await item.Locator(CartItemDescription).InnerTextAsync();
    }

    public async Task<string> GetItemPriceAsync(string name)
    {
        var item = await GetCartItemByNameAsync(name);
        return await item.Locator(CartItemPrice).InnerTextAsync();
    }

    public async Task<string> GetItemQuantityAsync(string name)
    {
        var item = await GetCartItemByNameAsync(name);
        return await item.Locator(CartItemQuantity).InnerTextAsync();
    }
}
