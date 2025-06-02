using ApplicationCenter.WebApp.Helper;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCenter.WebApp.Services;

internal class ConfigurationKeyService(IDbContextFactory<DatabaseContext> dbContextFactory)
{
    private readonly IDbContextFactory<DatabaseContext> _dbContextFactory = dbContextFactory;

    public async Task<List<ConfigurationKey>> GetConfigurationKeys(Guid applicationId)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var result = await context.ConfigurationKeys
            .Include(i => i.Values)
            .Where(i => i.ApplicationId == applicationId).OrderBy(i => i.Name)
            .ToListAsync();

        return result;
    }

    public async Task<ConfigurationKey> AddOrUpdateConfigurationKey(ConfigurationKey configurationKey, Guid applicationId)
    {
        ConfigurationKey dbEntity;

        await using var context = await _dbContextFactory.CreateDbContextAsync();
        if (configurationKey.Id == Guid.Empty)
        {
            dbEntity = new()
            {
                Id = Guid.CreateVersion7(),
                CreatedAt = DateTimeOffset.Now,
                UpdatedAt = DateTimeOffset.Now,
                Values = AddConfigurationKeyValues(),
                ApplicationId = applicationId,
            };

            await context.ConfigurationKeys.AddAsync(dbEntity);
        }
        else
        {
            dbEntity = await context.ConfigurationKeys
                .Include(i => i.Values)
                .FirstOrDefaultAsync(i => i.Id == configurationKey.Id) ?? throw new KeyNotFoundException($"No Configuration Key with id {configurationKey.Id} in database");
        }

        var updateCount = 0;
        dbEntity.Name = ComparisonHelper.TakeNewValueIfChanged(dbEntity.Name, configurationKey.Name, ref updateCount);
        dbEntity.Description = ComparisonHelper.TakeNewValueIfChanged(dbEntity.Description, configurationKey.Description, ref updateCount);

        if (updateCount > 0)
        {
            dbEntity.UpdatedAt = DateTimeOffset.Now;
            await context.SaveChangesAsync();
        }

        return dbEntity;
    }

    private static List<ConfigurationKeyValue> AddConfigurationKeyValues()
    {
        var result = new List<ConfigurationKeyValue>();
        var stages = Enum.GetValues<ApplicationStage>();
        foreach (var stage in stages)
        {
            var dbValue = new ConfigurationKeyValue
            {
                Id = Guid.CreateVersion7(),
                Stage = stage,
                Value = string.Empty,
                CreatedAt = DateTimeOffset.Now,
                UpdatedAt = DateTimeOffset.Now,
            };

            result.Add(dbValue);
        }

        return result;
    }

    public async Task RemoveConfigurationKey(Guid configurationKeyId)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var dbEntity = await context.ConfigurationKeys.FindAsync(configurationKeyId) ?? throw new KeyNotFoundException($"No Configuration Key with id {configurationKeyId} in database");
        context.ConfigurationKeys.Remove(dbEntity);
        await context.SaveChangesAsync();
    }
}