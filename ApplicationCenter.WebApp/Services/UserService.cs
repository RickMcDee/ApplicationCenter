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

        user.LastSeenAt = DateTimeOffset.Now;
        await context.SaveChangesAsync();

        result.Id = user.Id;
        result.Name = user.Name;
        result.EMail = user.EMail;
        result.IsKnown = true;

        return result;
    }

    public async Task<AppUserState> StoreNewUser(string userId, string userName, string userMail)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var user = await context.Users.AddAsync(new()
        {
            Id = Guid.CreateVersion7(),
            Name = userName,
            EMail = userMail,
            ExternalId = userId,
            AuthentificationProvider = "auth0",
            CreatedAt = DateTimeOffset.Now,
            LastSeenAt = DateTimeOffset.Now,
        });
        await context.SaveChangesAsync();

        return new()
        {
            Id = user.Entity.Id,
            Name = user.Entity.Name,
            EMail = user.Entity.EMail,
            IsKnown = true,
        };
    }
}