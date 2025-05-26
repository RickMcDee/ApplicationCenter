using Microsoft.EntityFrameworkCore;

namespace ApplicationCenter.WebApp.Database;

[PrimaryKey("Id")]
public class Application
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ApplicationType Type { get; set; } = ApplicationType.Unknown;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;

    public ICollection<ConfigurationKey> ConfigurationKeys { get; } = [];
}