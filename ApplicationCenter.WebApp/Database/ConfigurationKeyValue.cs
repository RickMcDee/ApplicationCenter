using Microsoft.EntityFrameworkCore;

namespace ApplicationCenter.WebApp.Database;

[PrimaryKey("Id")]
public class ConfigurationKeyValue
{
    public Guid Id { get; set; } = Guid.Empty;
    public ApplicationStage Stage { get; set; } = ApplicationStage.Development;
    public string Value { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;

    public Guid ConfigurationKeyId { get; set; }
    public ConfigurationKey ConfigurationKey { get; set; } = null!;
}