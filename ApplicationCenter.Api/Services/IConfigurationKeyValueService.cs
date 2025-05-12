namespace ApplicationCenter.Api.Services;

public interface IConfigurationKeyValueService
{
    Task<ConfigurationKeyValueViewModel> UpdateConfigurationKeyValue(ConfigurationKeyValueViewModel configurationKeyValue);
}