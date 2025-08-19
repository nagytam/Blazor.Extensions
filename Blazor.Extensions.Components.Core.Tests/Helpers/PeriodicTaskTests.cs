using System;
using System.Threading;
using System.Threading.Tasks;
using Blazor.Extensions.Components.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blazor.Extensions.Components.Core.Tests.Helpers;

[TestClass]
public class PeriodicTaskTests
{
    [TestMethod]
    public void Constructor_WithNullAction_ThrowsException()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() => new PeriodicTask(null!, TimeSpan.FromMilliseconds(10)));
    }

    [TestMethod]
    public async Task WhenInactive_ActionIsNotInvoked()
    {
        // Arrange
        var invoked = 0;
        using var cancellationTokenSource = new CancellationTokenSource();
        _ = new PeriodicTask(() => Interlocked.Increment(ref invoked), TimeSpan.FromMilliseconds(10))
        {
            IsActive = false,
            CancellationToken = cancellationTokenSource.Token
        };

        // Act - wait longer than a couple of periods
        await Task.Delay(60);
        var beforeCancel = Volatile.Read(ref invoked);

        // Cancel and give time to exit
        await cancellationTokenSource.CancelAsync();
        await Task.Delay(30);

        // Assert
        Assert.AreEqual(0, beforeCancel, "Action should not be invoked while inactive.");
    }

    [TestMethod]
    public async Task WhenActive_ActionIsInvoked_Periodically()
    {
        // Arrange
        var count = 0;
        var taskCompletionSource = new TaskCompletionSource<int>(TaskCreationOptions.RunContinuationsAsynchronously);
        using var cancellationTokenSourceTask = new CancellationTokenSource();

        _ = new PeriodicTask(() =>
        {
            var c = Interlocked.Increment(ref count);
            if (c >= 3)
            {
                taskCompletionSource.TrySetResult(c);
            }
        }, TimeSpan.FromMilliseconds(5))
        {
            IsActive = true,
            CancellationToken = cancellationTokenSourceTask.Token
        };

        // Act
        using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(2));
        using (cancellationTokenSourceTask.Token.Register(() => taskCompletionSource.TrySetCanceled()))
        {
            var result = await taskCompletionSource.Task; // throws if canceled
            Assert.IsTrue(result >= 3);
        }

        // Cancel and allow the loop to stop. Give a stabilization window to avoid race with a pending iteration.
        await cancellationTokenSourceTask.CancelAsync();
        await Task.Delay(30);
        var snapshot = Volatile.Read(ref count);
        await Task.Delay(30);
        var after = Volatile.Read(ref count);

        // Assert - no further increments after cancellation window
        Assert.AreEqual(snapshot, after);
    }

    [TestMethod]
    public async Task Toggle_IsActive_ControlsInvocation()
    {
        // Arrange
        var count = 0;
        using var cancellationTokenSource = new CancellationTokenSource();
        var periodicTask = new PeriodicTask(() => Interlocked.Increment(ref count), TimeSpan.FromMilliseconds(5))
        {
            IsActive = true,
            CancellationToken = cancellationTokenSource.Token
        };

        // Wait until at least one invocation occurs (avoid timing flakiness)
        var started = await WaitUntilAsync(() => Volatile.Read(ref count) > 0, TimeSpan.FromMilliseconds(500));
        Assert.IsTrue(started, "Expected some invocations when active.");
        var afterActive = Volatile.Read(ref count);
        Assert.IsTrue(afterActive > 0, "Expected some invocations when active.");

        // Deactivate and ensure no more increments after a few periods
        periodicTask.IsActive = false;
        var snapshot = Volatile.Read(ref count);
        await Task.Delay(60);
        var afterInactive = Volatile.Read(ref count);
        Assert.AreEqual(snapshot, afterInactive, "Count should not increase while inactive.");

        // Reactivate and confirm increments resume
        periodicTask.IsActive = true;
        var resumed = await WaitUntilAsync(() => Volatile.Read(ref count) > afterInactive, TimeSpan.FromMilliseconds(500));
        Assert.IsTrue(resumed, "Expected invocations after reactivation.");

        // Cleanup
        await cancellationTokenSource.CancelAsync();
        await Task.Delay(20);
    }

    private static async Task<bool> WaitUntilAsync(Func<bool> condition, TimeSpan timeout)
    {
        var start = DateTime.UtcNow;
        while (DateTime.UtcNow - start < timeout)
        {
            if (condition()) return true;
            await Task.Delay(10);
        }
        return condition();
    }
}
