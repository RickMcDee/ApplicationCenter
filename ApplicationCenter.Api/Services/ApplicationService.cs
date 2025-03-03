using ApplicationCenter.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCenter.Api.Services;

internal class ApplicationService(IDbContextFactory<Database.DatabaseContext> dbContextFactory) : IApplicationService
{
    private readonly IDbContextFactory<Database.DatabaseContext> _dbContextFactory = dbContextFactory;

    public async Task<IEnumerable<Application>> GetApplications()
    {
        var context = await _dbContextFactory.CreateDbContextAsync();
        var result = await context.Applications.Select(i => new Application { Id = i.Id, Name = i.Name }).ToListAsync();

        return result;
    }
}
