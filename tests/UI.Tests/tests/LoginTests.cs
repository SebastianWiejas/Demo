using UI.Tests.helpers;
using UI.Tests.Pages;
namespace UI.Tests.Tests;

public class LoginTests : TestsBase
{
    public override async Task InitializeAsync()
    {
        await base.InitializeAsync().ConfigureAwait(false);
    }
    
    public static IEnumerable<object[]> LoginTestData
    {
        get
        {
            return Configuration.Credentials!.Select(cred => new object[] { cred.Username, cred.Password, cred.Valid });
        }
    }

    [Theory]
    [MemberData(nameof(LoginTestData))]
    public async Task BasicLoginTest(string username, string password, bool shouldSucceed)
    {
        var loginPage = new LoginPage(Page);
        await loginPage.GoToAsync();
        await loginPage.Login(username, password);

        if(shouldSucceed)
        {
            var errorMessages = await loginPage.GetErrorMessage();
            Assert.Empty(errorMessages);
            var inventoryItemLink = Page.Locator("#item_0_img_link"); //TODO: Refactor this to InventoryPage
            Assert.True(await inventoryItemLink.IsVisibleAsync());
        }
        else
        {
            var errorMessages = await loginPage.GetErrorMessage();
            Assert.NotEmpty(errorMessages);
        }
    }
}