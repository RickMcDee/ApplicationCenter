namespace ApplicationCenter.Shared.Models;
public class ConfigurationKeyViewModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;
    public List<ConfigurationKeyValueViewModel> Values { get; set; } = [];
}
