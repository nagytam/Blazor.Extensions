using Blazor.Extensions.Application.Services;
using Blazor.Extensions.Infrastructure.Data;
using Microsoft.AspNetCore.Components;

namespace Blazor.Extensions.App.Server.Components.Pages;

public class DiagnosticsComponent : ComponentBase
{
    [Inject] FileService FileService{ get; set; }

    protected List<PersistedFileInformation> PersistedFileInformations => FileService.PersistedFileInformations;

    protected void OnFilesChanged()
    {
        InvokeAsync(() => StateHasChanged());
    }
}
