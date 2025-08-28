using Blazor.Extensions.Components.Core.Controls;

namespace Blazor.Extensions.Components.Test.Commands;

internal sealed class TestControl : ControlBase
{
    public int RefreshCount => Volatile.Read(ref _refreshCount);
    private int _refreshCount;

    public override void Refresh()
    {
        Interlocked.Increment(ref _refreshCount);
    }
}
