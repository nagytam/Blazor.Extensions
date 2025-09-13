namespace Blazor.Extensions.Infrastructure.Data;

public class PersistedFile : IAuditable
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public byte[] Content { get; set; }

    #region IAuditable
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }
    #endregion
}