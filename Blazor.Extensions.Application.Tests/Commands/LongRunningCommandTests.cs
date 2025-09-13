namespace Blazor.Extensions.Application.Tests.Commands;

[TestClass]
public class LongRunningCommandTests
{
    [TestMethod]
    public async Task Start_EnterInProgress_ThenCancel_ResetsState_And_Refreshes()
    {
        // Arrange
        var control = new TestControl();
        var cmd = new Application.Commands.LongRunningCommand(control);

        // Initial state
        Assert.IsFalse(cmd.IsInProgress);
        Assert.AreEqual("Long running command", cmd.Label);
        Assert.AreEqual("fa-solid fa-lock", cmd.IconCss);
        Assert.IsFalse(cmd.IsDisabled);

        // Act - start
        await cmd.OnClickAsync(new Microsoft.AspNetCore.Components.Web.MouseEventArgs());

        // Wait until running
        Assert.IsTrue(await WaitUntilAsync(() => cmd.IsInProgress, TimeSpan.FromSeconds(2)), "Command did not start in time.");

        // In-progress UI state
        Assert.AreEqual("Esc", cmd.AccessKey);
        Assert.AreEqual("e-danger", cmd.CssClass);
        StringAssert.StartsWith(cmd.Content, "CANCEL");
        Assert.AreEqual("fa-solid fa-xmark", cmd.IconCss);

        // Allow some refreshes while running
        await Task.Delay(250);
        var refreshesWhileRunning = control.RefreshCount;
        Assert.IsTrue(refreshesWhileRunning >= 1, "Expected at least one periodic Refresh while running.");

        // Act - cancel by clicking again
        await cmd.OnClickAsync(new Microsoft.AspNetCore.Components.Web.MouseEventArgs());

        // Wait until finished
        Assert.IsTrue(await WaitUntilAsync(() => !cmd.IsInProgress, TimeSpan.FromSeconds(3)), "Command did not finish in time after cancellation.");

        // Final state
        Assert.IsFalse(cmd.IsInProgress);
        Assert.AreEqual("fa-solid fa-lock", cmd.IconCss);
        Assert.IsFalse(cmd.IsDisabled);
        Assert.IsTrue(control.RefreshCount >= refreshesWhileRunning);
    }

    private static async Task<bool> WaitUntilAsync(Func<bool> condition, TimeSpan timeout, TimeSpan? pollInterval = null)
    {
        var interval = pollInterval ?? TimeSpan.FromMilliseconds(10);
        var start = DateTime.UtcNow;
        while (DateTime.UtcNow - start < timeout)
        {
            if (condition()) return true;
            await Task.Delay(interval);
        }
        return condition();
    }
}
