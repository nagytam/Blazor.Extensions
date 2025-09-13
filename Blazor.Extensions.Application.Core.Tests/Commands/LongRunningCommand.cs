using Blazor.Extensions.Application.Core.Commands;
using Blazor.Extensions.Application.Core.Controls;

namespace Blazor.Extensions.Application.Core.Tests.Commands;

public sealed class LongRunningCommand : CommandBase
{
    public LongRunningCommand(IRefreshable control) : base(control)
    {
    }

    public override string Label => "Run";
    protected override string HotKey { get; set; } = "a";

    public override async Task OnExecuteAsync(CancellationToken cancellationToken)
    {
        try
        {
            await Task.Delay(Timeout.Infinite, cancellationToken);
        }
        catch (TaskCanceledException)
        {
            // expected
        }
    }
}
