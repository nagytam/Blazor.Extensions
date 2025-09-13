using Blazor.Extensions.Application.Core.Commands;
using Blazor.Extensions.Application.Core.Controls;

namespace Blazor.Extensions.Application.Commands;

public class LongRunningCommand : CommandBase
{
    public override string Label => "Long running command";

    public LongRunningCommand() : base(null)
    {

    }

    public LongRunningCommand(IRefreshable refreshable) : base(refreshable)
    {

    }

    public override async Task OnExecuteAsync(CancellationToken cancellationToken)
    {
        try
        {
            await Task.Delay(5000, cancellationToken);
        }
        catch (TaskCanceledException)
        {
            // expected
        }
    }
}
