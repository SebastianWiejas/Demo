using Microsoft.Playwright;

namespace UI.Tests.Pages;

public class LoginPage : BasePage
{
    private readonly ILocator _usernameInput;
    private readonly ILocator _passwordInput;
    private readonly ILocator _loginButton;
    private readonly ILocator _errorContainer;
    private readonly ILocator _loginContainer;

    public LoginPage(IPage page) : base(page)
    {
        _loginContainer = _page.Locator("#login_button_container");
        _usernameInput = _loginContainer.GetByTestId("username");
        _passwordInput = _loginContainer.GetByTestId("password");
        _loginButton = _loginContainer.GetByTestId("login-button");
        _errorContainer = _loginContainer.GetByTestId("error");         
    }

    public async Task GoToAsync()
    {
        await _page.GotoAsync("/");
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