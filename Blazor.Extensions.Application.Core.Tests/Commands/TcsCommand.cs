using Blazor.Extensions.Application.Core.Commands;
using Blazor.Extensions.Application.Core.Controls;

namespace Blazor.Extensions.Application.Core.Tests.Commands;

public sealed class TcsCommand : CommandBase
{
    private readonly TaskCompletionSource _tcs;
    public TcsCommand(IRefreshable control, TaskCompletionSource tcs) : base(control)
    {
        _tcs = tcs;
    }

    public override string Label => "Run";
    protected override string HotKey { get; set; } = "a";

    public override Task OnExecuteAsync(CancellationToken cancellationToken)
    {
        return _tcs.Task.WaitAsync(cancellationToken);
    }
}
