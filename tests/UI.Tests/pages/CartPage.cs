using Microsoft.Playwright;
namespace UI.Tests.Pages;

public class CartPage
{
    private readonly IPage _page;

    private ILocator CartContainer => _page.GetByTestId("cart-contents-container");
    private ILocator CartItem => CartContainer.GetByTestId("inventory-item");
    private ILocator CartItemName => _page.GetByTestId("inventory-item-name");
    private ILocator CartItemDescription => CartItem.GetByTestId("inventory-item-desc");
    private ILocator CartItemPrice => CartItem.GetByTestId("inventory-item-price");
    private ILocator CartItemQuantity => CartItem.GetByTestId("item-quantity");
    private ILocator CheckoutButton => CartContainer.GetByTestId("checkout");
    private string RemoveButtonText => "Remove";

    public CartPage(IPage page)
    {
        _page = page;
    }

    public async Task GoToAsync()
    {
        await _page.GotoAsync("/cart.html");
    }

    public async Task<int> GetNumberOfItemsInCartAsync()
    {
        return await CartItem.CountAsync();
    }

    private async Task<ILocator> GetCartItemByNameAsync(string name)
    {
        var items = CartItemName
            .Filter(new LocatorFilterOptions { HasTextString = name });

        // if (items.Count == 0)
        // {
        //     throw new Exception($"There is no cart item with name '{name}'");
        // }
        // if (items.Count > 1)
        // {
        //     throw new Exception($"There is more than one cart item with name '{name}'");
        // }

        return CartItem.Filter(new LocatorFilterOptions { Has = items }).First;
    }

    public async Task ProceedToCheckoutAsync()
    {
        await CheckoutButton.ClickAsync();
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
