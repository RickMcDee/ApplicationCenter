using Microsoft.EntityFrameworkCore;

namespace ApplicationCenter.Api.Services;

internal class ConfigurationKeyService(IDbContextFactory<Database.DatabaseContext> dbContextFactory) : IConfigurationKeyService
{
    private readonly IDbContextFactory<Database.DatabaseContext> _dbContextFactory = dbContextFactory;

    public async Task<ConfigurationKeyViewModel> AddOrUpdateConfigurationKey(ConfigurationKeyViewModel configurationKey, Guid? applicationId)
    {
        Database.ConfigurationKey dbEntity;

        await using var context = await _dbContextFactory.CreateDbContextAsync();
        if (configurationKey.Id == Guid.Empty && applicationId.HasValue)
        {
            dbEntity = new()
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTimeOffset.Now,
                UpdatedAt = DateTimeOffset.Now,
                Values = AddConfigurationKeyValues(),
                ApplicationId = applicationId.Value
            };

            await context.ConfigurationKeys.AddAsync(dbEntity);
        }
        else if (configurationKey.Id != Guid.Empty)
        {
            dbEntity = await context.ConfigurationKeys
                .Include(i => i.Values)
                .FirstOrDefaultAsync(i => i.Id == configurationKey.Id) ?? throw new KeyNotFoundException($"No Configuration Key with id {configurationKey.Id} in database");
        }
        else
        {
            throw new Exception("ConfigurationKeyId and ApplicationId cannot both be empty");
        }

        var updateCount = 0;
        dbEntity.Name = ComparisonHelper.TakeNewValueIfChanged(dbEntity.Name, configurationKey.Name, ref updateCount);
        dbEntity.Description = ComparisonHelper.TakeNewValueIfChanged(dbEntity.Description, configurationKey.Description, ref updateCount);

        if (updateCount > 0)
        {
            dbEntity.UpdatedAt = DateTimeOffset.Now;
            await context.SaveChangesAsync();
        }

        return dbEntity.ToViewModel();
    }

    private static List<Database.ConfigurationKeyValue> AddConfigurationKeyValues()
    {
        var result = new List<Database.ConfigurationKeyValue>();
        var stages = Enum.GetValues<ApplicationStage>();
        foreach (var stage in stages)
        {
            var dbValue = new Database.ConfigurationKeyValue()
            {
                Id = Guid.NewGuid(),
                Stage = stage,
                Value = string.Empty,
                CreatedAt = DateTimeOffset.Now,
                UpdatedAt = DateTimeOffset.Now,
            };

            result.Add(dbValue);
        }

        return result;
    }

    public async Task<IEnumerable<ConfigurationKeyViewModel>> GetConfigurationKeys(Guid applicationId)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var result = await context.ConfigurationKeys
            .Include(i => i.Values)
            .Where(i => i.ApplicationId == applicationId).OrderBy(i => i.Name)
            .Select(i => i.ToViewModel())
            .ToListAsync();

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