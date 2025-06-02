using Microsoft.EntityFrameworkCore;

namespace ApplicationCenter.WebApp.Helper;

public static class DependencyInjectionHelper
{
    internal static IServiceCollection AddDatabaseContext(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContextFactory<ConfigurationDataContext>(opt =>
        {
            opt.UseNpgsql(connectionString);
#if DEBUG
            opt.EnableDetailedErrors();
            opt.EnableSensitiveDataLogging();
#endif
        });

        services.AddDbContextFactory<UserDataContext>(opt =>
        {
            opt.UseNpgsql(connectionString);
#if DEBUG
            opt.EnableDetailedErrors();
            opt.EnableSensitiveDataLogging();
#endif
        });

        return services;
    }
}