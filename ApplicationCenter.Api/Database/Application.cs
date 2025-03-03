using ApplicationCenter.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCenter.Api.Database;

[PrimaryKey("Id")]
internal class Application
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;

    internal ApplicationViewModel ToViewModel()
    {
        return new ApplicationViewModel
        {
            Id = Id,
            Name = Name
        };
    }
}
