using Microsoft.EntityFrameworkCore;

namespace ApplicationCenter.WebApp.Database;

internal class UserDataContext(DbContextOptions<UserDataContext> options) : DbContext(options)
{
    static UserDataContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<AppUser> Users { get; set; }
}