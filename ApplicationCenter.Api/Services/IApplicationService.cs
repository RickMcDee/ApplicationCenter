using ApplicationCenter.Shared.Models;

namespace ApplicationCenter.Api.Services;

public interface IApplicationService
{
    Task<IEnumerable<Application>> GetApplications();
}
