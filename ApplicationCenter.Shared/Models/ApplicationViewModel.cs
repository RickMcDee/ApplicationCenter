using ApplicationCenter.Shared.Enums;

namespace ApplicationCenter.Shared.Models;

public class ApplicationViewModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public ApplicationType Type { get; set; } = ApplicationType.Unknown;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;
}
