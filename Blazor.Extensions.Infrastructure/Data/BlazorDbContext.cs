using Microsoft.EntityFrameworkCore;

namespace Blazor.Extensions.Infrastructure.Data;

public class BlazorDbContext(DbContextOptions<BlazorDbContext> options)
    : DbContext(options)
{
    public DbSet<PersistedFile> PersistedFiles { get; set; }
}
    
