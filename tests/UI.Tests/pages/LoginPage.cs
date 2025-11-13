using Microsoft.Playwright;

namespace UI.Tests.Pages;

public class LoginPage
{
    private readonly IPage _page;
    private readonly ILocator _usernameInput;
    private readonly ILocator _passwordInput;
    private readonly ILocator _loginButton;
    private readonly ILocator _errorContainer;

    private ILocator LoginContainer => _page.Locator("#login_button_container");


    public LoginPage(IPage page)
    {
        _page = page;
        this.GoToAsync().GetAwaiter().GetResult();

        _usernameInput = LoginContainer.Locator("#user-name");
        _passwordInput = LoginContainer.Locator("#password");
        _loginButton = LoginContainer.Locator("#login-button");
        _errorContainer = LoginContainer.Locator("h3[data-test='error']");
    }

    public async Task GoToAsync()
    {
        await _page.GotoAsync("https://www.saucedemo.com/");
    }

    public async Task EnterUsername(string username)
    {
        await _usernameInput.FillAsync(username);
    }

    public async Task EnterPassword(string password)
    {
        await _passwordInput.FillAsync(password);
    }

    public async Task ClickLogin()
    {
        await _loginButton.ClickAsync();
    }

    public async Task Login(string username, string password)
    {
        await EnterUsername(username);
        await EnterPassword(password);
        await ClickLogin();
    }

    public async Task<IReadOnlyList<string>> GetErrorMessage()
    {
        return await _errorContainer.AllInnerTextsAsync();
    }
}