using Blazor.Extensions.Application.Services;
using Blazor.Extensions.Infrastructure.Data;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Inputs;

namespace Blazor.Extensions.Presentation.Components;

public class FileUploadComponent : ComponentBase
{
    [Inject]
    private FileService FileService { get; set; }

    protected async Task OnChange(UploadChangeEventArgs args)
    {
        try
        {
            foreach (var file in args.Files)
            {
                var persistedFile = new PersistedFile
                {
                    FileName = file.FileInfo.Name,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedBy = string.Empty, // TODO
                    UpdatedBy = string.Empty  // TODO
                };
                using (var stream = file.File.OpenReadStream(long.MaxValue))
                {
                    using var memoryStream = new MemoryStream();
                    await stream.CopyToAsync(memoryStream);
                    persistedFile.Content = memoryStream.ToArray();
                }
                await FileService.UploadFileAsync(persistedFile);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
