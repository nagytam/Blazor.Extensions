using Microsoft.AspNetCore.Components;

namespace Blazor.Extensions.Application.Shared.Controls;

public class ControlBase : ComponentBase, IRefreshable
{
    public virtual void Refresh()
    {
        StateHasChanged();
    }
}
