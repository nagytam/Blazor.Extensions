using Blazor.Extensions.Application.Shared.Commands;
using Blazor.Extensions.Application.Shared.Controls;

namespace Blazor.Extensions.Application.Shared.Tests.Commands;

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
