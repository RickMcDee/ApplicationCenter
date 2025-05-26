using Microsoft.EntityFrameworkCore;

namespace ApplicationCenter.WebApp.Database;

[PrimaryKey("Id")]
public class ConfigurationKey
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;

    public Guid ApplicationId { get; set; }
    public Application Application { get; set; } = null!;

    public ICollection<ConfigurationKeyValue> Values { get; set; } = [];
}
