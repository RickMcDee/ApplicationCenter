using Microsoft.EntityFrameworkCore;

namespace ApplicationCenter.WebApp.Database;

internal class ConfigurationDataContext(DbContextOptions<ConfigurationDataContext> options) : DbContext(options)
{
    static ConfigurationDataContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<Application> Applications { get; set; }
    public DbSet<ConfigurationKey> ConfigurationKeys { get; set; }
    public DbSet<ConfigurationKeyValue> ConfigurationKeyValues { get; set; }
}