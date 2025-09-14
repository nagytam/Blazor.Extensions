using Blazor.Extensions.Application.Shared.Controls;

namespace Blazor.Extensions.Application.Shared.Tests.Commands;

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
