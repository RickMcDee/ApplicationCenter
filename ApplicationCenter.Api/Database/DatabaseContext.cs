using Microsoft.EntityFrameworkCore;

namespace ApplicationCenter.Api.Database;

internal class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Application> Applications { get; set; }
    public DbSet<ConfigurationKey> ConfigurationKeys { get; set; }
}
