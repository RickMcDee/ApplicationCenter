using Microsoft.EntityFrameworkCore;

namespace ApplicationCenter.Api.Database;

[PrimaryKey("Id")]
internal class Application
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ApplicationType Type { get; set; } = ApplicationType.Unknown;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;

    public ICollection<ConfigurationKey> ConfigurationKeys { get; } = [];

    internal ApplicationViewModel ToViewModel()
    {
        return new ApplicationViewModel
        {
            Id = Id,
            Name = Name,
            Description = Description,
            Type = Type,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt
        };
    }
}
