using ApplicationCenter.WebApp.Helper;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCenter.WebApp.Services;

internal class ConfigurationKeyValueService(IDbContextFactory<DatabaseContext> dbContextFactory)
{
    private readonly IDbContextFactory<DatabaseContext> _dbContextFactory = dbContextFactory;

    public async Task<ConfigurationKeyValue> UpdateConfigurationKeyValue(ConfigurationKeyValue configurationKeyValue)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var keyValue = await context.ConfigurationKeyValues.FindAsync(configurationKeyValue.Id) ?? throw new KeyNotFoundException($"ConfigurationKeyValue {configurationKeyValue.Id} not found");

        var updateCount = 0;
        keyValue.Value = ComparisonHelper.TakeNewValueIfChanged(keyValue.Value, configurationKeyValue.Value, ref updateCount);

        if (updateCount > 0)
        {
            keyValue.UpdatedAt = DateTimeOffset.Now;
            await context.SaveChangesAsync();
        }

        return keyValue;
    }
}