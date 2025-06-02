using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCenter.WebApp.Database.UserData;

[PrimaryKey("Id")]
[Table("User", Schema = "userdata")]
public class AppUser
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public string EMail { get; set; } = string.Empty;
    public string ExternalId { get; set; } = string.Empty;
    public string AuthentificationProvider { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset LastSeenAt { get; set; } = DateTimeOffset.Now;
}