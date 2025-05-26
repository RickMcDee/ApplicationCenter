using Microsoft.EntityFrameworkCore;

namespace ApplicationCenter.WebApp.Database;

internal class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    static DatabaseContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<Application> Applications { get; set; }
    public DbSet<ConfigurationKey> ConfigurationKeys { get; set; }
    public DbSet<ConfigurationKeyValue> ConfigurationKeyValues { get; set; }
}