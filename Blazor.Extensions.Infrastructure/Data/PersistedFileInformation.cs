namespace Blazor.Extensions.Infrastructure.Data;

public class PersistedFileInformation : IAuditable
{
    public int Id { get; set; }
    public string FileName { get; set; }

    #region IAuditable
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }
    #endregion
}
