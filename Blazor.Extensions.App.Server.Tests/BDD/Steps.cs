using Blazor.Extensions.App.Server.Tests.Initialization;
using Microsoft.Playwright;
using Reqnroll;
using System.Diagnostics;

namespace Blazor.Extensions.App.Server.Tests.BDD;

[Binding]
public sealed class Steps
{
    private IPage? _page;

    [BeforeScenario(Order = 1)]
    public void FirstBeforeScenario()
    {
        Debug.Assert(Global.AppRunner != null, "Web application is not running.");
        Debug.Assert(Global.playwright != null, "Playwright is not initialized.");
    }

    [AfterScenario]
    public async Task AfterScenario()
    {
    }

    [Given("Web application is running")]
    public void GivenTheWebApplicationIsRunning()
    {
        Debug.Assert(Global.AppRunner != null, "Web application is not running.");
        Debug.Assert(Global.Browser != null, "Browser is not initialized.");
    }

    [When("user navigates to the homepage")]
    public async Task WhenTheUserNavigatesToTheHomepage()
    {
        Debug.Assert(Global.AppRunner != null, "Web application is not running.");
        Debug.Assert(Global.Browser != null, "Browser is not initialized.");

        var url = "http://localhost:5022/";
        _page = await Global.Browser.NewPageAsync();
        await _page.GotoAsync(url);
    }

    [Then("homepage should be displayed with the welcome text")]
    public async Task ThenHomepageShouldBeDisplayed()
    {   
        Debug.Assert(_page != null, "Web page is not initialized.");
        await _page.WaitForSelectorAsync("text=Welcome to your new app!");
    }
}