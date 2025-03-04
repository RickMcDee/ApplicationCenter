using ApplicationCenter.Shared.Helper;
using ApplicationCenter.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCenter.Api.Services;

internal class ApplicationService(IDbContextFactory<Database.DatabaseContext> dbContextFactory) : IApplicationService
{
    private readonly IDbContextFactory<Database.DatabaseContext> _dbContextFactory = dbContextFactory;

    public async Task<ApplicationViewModel> AddOrUpdateApplication(ApplicationViewModel application)
    {
        Database.Application dbEntity;

        using var context = await _dbContextFactory.CreateDbContextAsync();
        if (application.Id == Guid.Empty)
        {
            dbEntity = new()
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTimeOffset.Now,
                UpdatedAt = DateTimeOffset.Now
            };

            await context.Applications.AddAsync(dbEntity);
        }
        else
        {
            dbEntity = await context.Applications.FindAsync(application.Id) ?? throw new Exception($"No Application with id {application.Id} in database");
        }

        var updateCount = 0;
        dbEntity.Name = ComparisonHelper.TakeNewValueIfChanged(dbEntity.Name, application.Name, ref updateCount);
        dbEntity.Type = ComparisonHelper.TakeNewValueIfChanged(dbEntity.Type, application.Type, ref updateCount);
        dbEntity.Description = ComparisonHelper.TakeNewValueIfChanged(dbEntity.Description, application.Description, ref updateCount);

        if (updateCount > 0)
        {
            dbEntity.UpdatedAt = DateTimeOffset.Now;
            await context.SaveChangesAsync();
        }

        return dbEntity.ToViewModel();
    }

    public async Task<IEnumerable<ApplicationViewModel>> GetApplications()
    {
        var context = await _dbContextFactory.CreateDbContextAsync();
        var result = await context.Applications.Select(i => i.ToViewModel()).ToListAsync();

        return result;
    }

    public async Task RemoveApplication(Guid applicationId)
    {
        var context = await _dbContextFactory.CreateDbContextAsync();
        var dbEntity = await context.Applications.FindAsync(applicationId) ?? throw new Exception($"No Application with id {applicationId} in database");
        context.Applications.Remove(dbEntity);
        context.SaveChanges();
    }
}
