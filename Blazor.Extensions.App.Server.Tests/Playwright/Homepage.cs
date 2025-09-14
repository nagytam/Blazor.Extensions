using Blazor.Extensions.App.Server.Tests.Initialization;

namespace Blazor.Extensions.App.Server.Tests;

[TestClass]
public sealed class Homepage
{
    [TestMethod]
    public async Task HomePage()
    {
        var url = "http://localhost:5022/";
        Assert.IsNotNull(Global.Browser);

        //Arrange  
        var page = await Global.Browser.NewPageAsync();

        //Act  
        await page.GotoAsync(url);

        //Assert  
        await page.WaitForSelectorAsync("text=Welcome to your new app!");
    }
}

