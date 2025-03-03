using ApplicationCenter.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCenter.Api.Database;

[PrimaryKey("Id")]
internal class ConfigurationKey
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;
    public string Value { get; set; } = string.Empty;

    public Guid ApplicationId { get; set; }
    public Application Application { get; set; } = null!;

    internal ConfigurationKeyViewModel ToViewModel()
    {
        return new ConfigurationKeyViewModel
        {
            Id = Id,
            Name = Name,
            Value = Value
        };
    }
}
