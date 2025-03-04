namespace ApplicationCenter.Api.Services;

public interface IApplicationService
{
    Task<ApplicationViewModel> AddOrUpdateApplication(ApplicationViewModel application);
    Task<IEnumerable<ApplicationViewModel>> GetApplications();
    Task RemoveApplication(Guid applicationId);
}
