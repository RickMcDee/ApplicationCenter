using ApplicationCenter.Shared.Enums;

namespace ApplicationCenter.Shared.Models;

public class ConfigurationKeyValueViewModel
{
    public Guid Id { get; set; }
    public string Value { get; set; } = string.Empty;
    public ApplicationStage Stage { get; set; } = ApplicationStage.Development;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;
}
