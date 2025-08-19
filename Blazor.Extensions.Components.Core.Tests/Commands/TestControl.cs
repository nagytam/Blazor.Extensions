using Blazor.Extensions.Components.Core.Controls;

namespace Blazor.Extensions.Components.Core.Tests.Commands;

public sealed class TestControl : ControlBase
{
    public int RefreshCount => Volatile.Read(ref _refreshCount);
    private int _refreshCount;

    public TestControl()
    {
        // Logger = new TestLogger();
    }

    public override void Refresh()
    {
        Interlocked.Increment(ref _refreshCount);
    }
}
