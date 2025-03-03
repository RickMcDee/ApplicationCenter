using ApplicationCenter.Shared.Models;

namespace ApplicationCenter.Api.Services;

public interface IConfigurationKeyService
{
    Task<ConfigurationKeyViewModel> AddOrUpdateConfigurationKey(ConfigurationKeyViewModel configurationKey, Guid applicationId);
    Task<IEnumerable<ConfigurationKeyViewModel>> GetConfigurationKeys(Guid applicationId);
    Task RemoveConfigurationKey(Guid configurationKeyId);
}
