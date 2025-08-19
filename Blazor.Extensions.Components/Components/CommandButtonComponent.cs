using Blazor.Extensions.Components.Core.Commands;
using Blazor.Extensions.Components.Core.Controls;
using Microsoft.AspNetCore.Components;

namespace Blazor.Extensions.Components.Components;

public class CommandButtonComponent : ComponentBase, IRefreshable
{
    [Parameter] public CommandBase Command { get; set; } = null!;

    protected override void OnInitialized()
    {
        if (Command != null)
        {
            Command.Control = this;
        }
        base.OnInitialized();
    }

    public void Refresh()
    {
        InvokeAsync(() => StateHasChanged());
    }
}
