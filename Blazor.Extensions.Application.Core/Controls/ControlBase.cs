using Microsoft.AspNetCore.Components;

namespace Blazor.Extensions.Application.Core.Controls;

public class ControlBase : ComponentBase, IRefreshable
{
    //[Inject]
    //public ILogger Logger { get; set; }

    public virtual void Refresh()
    {
        StateHasChanged();
    }
}
