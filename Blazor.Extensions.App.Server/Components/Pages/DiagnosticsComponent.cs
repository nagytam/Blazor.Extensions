using Blazor.Extensions.Infrastructure.Data;
using Microsoft.AspNetCore.Components;

namespace Blazor.Extensions.App.Server.Components.Pages;

public class DiagnosticsComponent : ComponentBase
{
    [Inject] BlazorDbContext DbContext { get; set; }
}
