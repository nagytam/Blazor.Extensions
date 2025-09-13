using Blazor.Extensions.Infrastructure.Data;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Inputs;

namespace Blazor.Extensions.Presentation.Components;

public class FileUploadComponent : ComponentBase
{
    [Inject]
    protected BlazorDbContext DbContext { get; set; }
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
                DbContext.PersistedFiles.Add(persistedFile);
                await DbContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
