namespace ApplicationCenter.Shared.Models;
public class ConfigurationKeyViewModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string Value { get; set; } = string.Empty;
}
