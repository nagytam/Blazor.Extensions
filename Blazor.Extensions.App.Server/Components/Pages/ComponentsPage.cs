using Blazor.Extensions.Application.Commands;
using Blazor.Extensions.Application.Shared.Controls;

namespace Blazor.Extensions.App.Server.Components.Pages;

public class ComponentsPage : ControlBase
{
    protected LongRunningCommand LongRunningCommand { get; set; }

    public ComponentsPage()
    {
        LongRunningCommand = new LongRunningCommand();
    }
}
