using Microsoft.EntityFrameworkCore;

namespace ApplicationCenter.Api.Database;

[PrimaryKey("Id")]
internal class ConfigurationKeyValue
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public ApplicationStage Stage { get; set; } = ApplicationStage.Development;
    public string Value { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;

    public Guid ConfigurationKeyId { get; set; }
    public ConfigurationKey ConfigurationKey { get; set; } = null!;

    internal ConfigurationKeyValueViewModel ToViewModel()
    {
        return new ConfigurationKeyValueViewModel
        {
            Id = Id,
            Stage = Stage,
            Value = Value,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt,
        };
    }
}
