using Blazor.Extensions.App.Server.Tests.Helpers;
using Microsoft.Playwright;
using Reqnroll;

namespace Blazor.Extensions.App.Server.Tests.Initialization;

[Binding]
public static class Global
{
    public static WebAppRunner? AppRunner;
    public static IPlaywright? playwright;
    public static IBrowser? Browser;

    [BeforeTestRun]
    public static void AssemblyInitialize()
    {
        var taskServer = Task.Run(() =>
        {
            AppRunner = new WebAppRunner();
            AppRunner.Run();
        });
        var taskBrowser = Task.Run(async () =>
        {
            playwright = await Playwright.CreateAsync();
            Browser = await playwright.Chromium.LaunchAsync(
                new BrowserTypeLaunchOptions { Headless = false });
        });
        Task.WaitAll(taskServer, taskBrowser);
    }

    [AfterTestRun]
    public static async Task AssemblyCleanup() 
    {
        AppRunner?.Stop();
        if (Browser is not null)
        {
            await Browser.DisposeAsync();
            Browser = null;
        }
        if (playwright is not null)
        {
            playwright.Dispose();
            playwright = null;
        }
    }
}
