using Blazor.Extensions.Infrastructure.Data;

namespace Blazor.Extensions.Application.Services;

public class FileService
{
    protected BlazorDbContext DbContext { get; set; }

    public List<PersistedFileInformation> PersistedFileInformations 
    { 
        get
        {
            return DbContext.PersistedFiles.Cast<PersistedFileInformation>().ToList();
        }
    }

    public FileService(BlazorDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task UploadFileAsync(PersistedFile persistedFile)
    {
        DbContext.PersistedFiles.Add(persistedFile);
        await DbContext.SaveChangesAsync();
    }
}
