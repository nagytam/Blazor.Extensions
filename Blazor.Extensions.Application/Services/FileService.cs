using Blazor.Extensions.Infrastructure.Data;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Extensions.Application.Services
{
    public class FileService
    {
        protected BlazorDbContext DbContext { get; set; }

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
}
