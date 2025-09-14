using System.Diagnostics;
using Microsoft.AspNetCore.Components.Web;

namespace Blazor.Extensions.Application.Shared.Tests.Commands;

[TestClass]
public partial class CommandBaseTests
{
    [TestMethod]
    public async Task Start_LongRunningCommand_SetsInProgress_UIState_And_Refreshes()
    {
        // Arrange
        var control = new TestControl();
        var cmd = new LongRunningCommand(control);

        // Initial state
        Assert.IsFalse(cmd.IsInProgress);
        Assert.AreEqual("fa-solid fa-lock", cmd.IconCss);
        Assert.AreEqual("Run", cmd.Content);
        Assert.AreEqual("A", cmd.AccessKey);
        Assert.AreEqual(string.Empty, cmd.Title);
        Assert.IsFalse(cmd.IsDisabled);

        // Act - start
        await cmd.OnClickAsync(new MouseEventArgs());

        // Wait until running
        Assert.IsTrue(await WaitUntilAsync(() => cmd.IsInProgress, TimeSpan.FromSeconds(2)), "Command did not enter InProgress state in time.");

        // Assert in-progress UI state
        Assert.AreEqual("Esc", cmd.AccessKey, "AccessKey should be Esc while running.");
        Assert.AreEqual("e-danger", cmd.CssClass, "CssClass should indicate danger while running.");
        StringAssert.StartsWith(cmd.Content, "CANCEL");
        Assert.AreEqual("fa-solid fa-xmark", cmd.IconCss, "Icon should be cancel icon while running (before cancellation).");

        // Allow periodic refresh to run at least once (CommandBase uses 100ms period)
        await Task.Delay(250);
        var refreshesWhileRunning = control.RefreshCount;
        Assert.IsTrue(refreshesWhileRunning >= 1, "Expected at least one periodic Refresh while running.");

        // Act - request cancel (second click)
        await cmd.OnClickAsync(new MouseEventArgs());

        // Wait until completed
        Assert.IsTrue(await WaitUntilAsync(() => !cmd.IsInProgress, TimeSpan.FromSeconds(2)), "Command did not finish in time after cancellation.");

        // Final state after cancellation completes
        Assert.IsFalse(cmd.IsInProgress);
        Assert.AreEqual("A", cmd.AccessKey);
        Assert.AreEqual("Run", cmd.Content);
        Assert.AreEqual("fa-solid fa-lock", cmd.IconCss);
        Assert.IsFalse(cmd.IsDisabled, "Should not be disabled after completion.");

        // Final refresh should have occurred
        Assert.IsTrue(control.RefreshCount >= refreshesWhileRunning, "Refresh count should not decrease after completion.");
    }

    [TestMethod]
    public async Task TcsCommand_Completes_Resets_UIState_And_Refreshes()
    {
        // Arrange
        var control = new TestControl();
        var tcs = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var cmd = new TcsCommand(control, tcs);

        // Act - start
        await cmd.OnClickAsync(new MouseEventArgs());

        // Wait until running
        Assert.IsTrue(await WaitUntilAsync(() => cmd.IsInProgress, TimeSpan.FromSeconds(2)), "Command did not start in time.");

        // Complete the operation
        tcs.TrySetResult();

        // Wait for completion
        Assert.IsTrue(await WaitUntilAsync(() => !cmd.IsInProgress, TimeSpan.FromSeconds(2)), "Command did not complete in time.");

        // Assert final UI state
        Assert.IsFalse(cmd.IsInProgress);
        Assert.AreEqual("A", cmd.AccessKey);
        Assert.AreEqual("Run", cmd.Content);
        Assert.AreEqual("fa-solid fa-lock", cmd.IconCss);
        Assert.IsFalse(cmd.IsDisabled);
        Assert.IsTrue(control.RefreshCount >= 1, "Expected at least one Refresh call by the end.");
    }

    [TestMethod]
    public void Id_ReturnsFullName_OfDerivedCommand()
    {
        var cmd = new LongRunningCommand(new TestControl());
        Assert.AreEqual(typeof(LongRunningCommand).FullName, cmd.Id);
    }

    [TestMethod]
    public async Task GetHotKeyTooltip_Reflects_AccessKey_State()
    {
        var control = new TestControl();
        var cmd = new LongRunningCommand(control);

        // Idle state
        StringAssert.Contains(cmd.GetHotKeyTooltip(), "Alt+A");

        // Start running
        await cmd.OnClickAsync(new MouseEventArgs());
        Assert.IsTrue(await WaitUntilAsync(() => cmd.IsInProgress, TimeSpan.FromSeconds(2)));
        StringAssert.Contains(cmd.GetHotKeyTooltip(), "Alt+Esc");

        // Cancel and verify back to idle tooltip
        await cmd.OnClickAsync(new MouseEventArgs());
        Assert.IsTrue(await WaitUntilAsync(() => !cmd.IsInProgress, TimeSpan.FromSeconds(2)));
        StringAssert.Contains(cmd.GetHotKeyTooltip(), "Alt+A");
    }

    private static async Task<bool> WaitUntilAsync(Func<bool> condition, TimeSpan timeout, TimeSpan? pollInterval = null)
    {
        var interval = pollInterval ?? TimeSpan.FromMilliseconds(10);
        var sw = Stopwatch.StartNew();
        while (sw.Elapsed < timeout)
        {
            if (condition())
            {
                return true;
            }
            await Task.Delay(interval);
        }
        return condition();
    }
}
