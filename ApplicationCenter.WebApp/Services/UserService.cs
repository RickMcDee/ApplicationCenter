using Microsoft.EntityFrameworkCore;

namespace ApplicationCenter.WebApp.Services;

internal class UserService(IDbContextFactory<UserDataContext> dbContextFactory)
{
    private readonly IDbContextFactory<UserDataContext> _dbContextFactory = dbContextFactory;

    internal async Task<AppUserState> GetUserData(string externalUserId)
    {
        var result = new AppUserState();

        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var user = await context.Users.FirstOrDefaultAsync(i => i.ExternalId == externalUserId);
        if (user is null)
        {
            return result;
        }

        result.Id = user.Id;
        result.Name = user.Name;
        result.EMail = user.EMail;
        result.IsKnown = true;

        return result;
    }

    public async Task<AppUserState> StoreNewUser(string userId, string userName, string userMail)
    {
        throw new NotImplementedException();
    }
}