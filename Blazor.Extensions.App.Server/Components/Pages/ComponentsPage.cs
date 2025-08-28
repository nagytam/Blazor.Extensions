using Blazor.Extensions.Components.Commands;
using Blazor.Extensions.Components.Core.Controls;

namespace Blazor.Extensions.App.Server.Components.Pages;

public class ComponentsPage : ControlBase
{
    protected LongRunningCommand LongRunningCommand { get; set; }

    public ComponentsPage()
    {
        LongRunningCommand = new LongRunningCommand();
    }
}
