using Blazor.Extensions.Application.Core.Controls;

namespace Blazor.Extensions.Application.Tests.Commands;

internal sealed class TestControl : ControlBase
{
    public int RefreshCount => Volatile.Read(ref _refreshCount);
    private int _refreshCount;

    public override void Refresh()
    {
        Interlocked.Increment(ref _refreshCount);
    }
}
