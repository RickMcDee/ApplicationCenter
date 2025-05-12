using Microsoft.EntityFrameworkCore;

namespace ApplicationCenter.Api.Database;

[PrimaryKey("Id")]
internal class ConfigurationKey
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;

    public Guid ApplicationId { get; set; }
    public Application Application { get; set; } = null!;

    public ICollection<ConfigurationKeyValue> Values { get; set; } = [];

    internal ConfigurationKeyViewModel ToViewModel()
    {
        return new ConfigurationKeyViewModel
        {
            Id = Id,
            Name = Name,
            Description = Description,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt,
            Values = [.. Values.Select(i => i.ToViewModel())]
        };
    }
}
