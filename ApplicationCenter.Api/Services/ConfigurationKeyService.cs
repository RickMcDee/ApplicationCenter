using ApplicationCenter.Shared.Helper;
using ApplicationCenter.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCenter.Api.Services;

internal class ConfigurationKeyService(IDbContextFactory<Database.DatabaseContext> dbContextFactory) : IConfigurationKeyService
{
    private readonly IDbContextFactory<Database.DatabaseContext> _dbContextFactory = dbContextFactory;

    public async Task<ConfigurationKeyViewModel> AddOrUpdateConfigurationKey(ConfigurationKeyViewModel configurationKey, Guid? applicationId)
    {
        Database.ConfigurationKey dbEntity;

        using var context = await _dbContextFactory.CreateDbContextAsync();
        if (configurationKey.Id == Guid.Empty && applicationId.HasValue)
        {
            dbEntity = new()
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTimeOffset.Now,
                UpdatedAt = DateTimeOffset.Now,
                ApplicationId = applicationId.Value
            };

            await context.ConfigurationKeys.AddAsync(dbEntity);
        }
        else if (configurationKey.Id != Guid.Empty)
        {
            dbEntity = await context.ConfigurationKeys.FindAsync(configurationKey.Id) ?? throw new Exception($"No Configuration Key with id {configurationKey.Id} in database");
        }
        else
        {
            throw new Exception("ConfigurationKeyId and ApplicationId cannot both be empty");
        }

        var updateCount = 0;
        dbEntity.Name = ComparisonHelper.TakeNewValueIfChanged(dbEntity.Name, configurationKey.Name, ref updateCount);
        dbEntity.Value = ComparisonHelper.TakeNewValueIfChanged(dbEntity.Value, configurationKey.Value, ref updateCount);
        dbEntity.Description = ComparisonHelper.TakeNewValueIfChanged(dbEntity.Description, configurationKey.Description, ref updateCount);

        if (updateCount > 0)
        {
            dbEntity.UpdatedAt = DateTimeOffset.Now;
            await context.SaveChangesAsync();
        }

        return dbEntity.ToViewModel();
    }

    public async Task<IEnumerable<ConfigurationKeyViewModel>> GetConfigurationKeys(Guid applicationId)
    {
        var context = await _dbContextFactory.CreateDbContextAsync();
        var result = await context.ConfigurationKeys.Where(i => i.ApplicationId == applicationId).Select(i => i.ToViewModel()).ToListAsync();

        return result;
    }

    public async Task RemoveConfigurationKey(Guid configurationKeyId)
    {
        var context = await _dbContextFactory.CreateDbContextAsync();
        var dbEntity = await context.ConfigurationKeys.FindAsync(configurationKeyId) ?? throw new Exception($"No Configuration Key with id {configurationKeyId} in database");
        context.ConfigurationKeys.Remove(dbEntity);
        context.SaveChanges();
    }
}
