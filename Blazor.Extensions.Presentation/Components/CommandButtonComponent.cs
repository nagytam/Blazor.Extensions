using Blazor.Extensions.Application.Shared.Commands;
using Blazor.Extensions.Application.Shared.Controls;
using Microsoft.AspNetCore.Components;

namespace Blazor.Extensions.Presentation.Components;

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
