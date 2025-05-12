namespace ApplicationCenter.Api.Services;

public interface IConfigurationKeyValueService
{
    /// <summary>
    /// Updates a ConfigurationKeyValue.
    /// </summary>
    /// <param name="configurationKeyValue">The model to update</param>
    /// <returns>The updated model</returns>
    /// <exception cref="KeyNotFoundException">There is no ConfigurationKeyValue with the given id</exception>
    Task<ConfigurationKeyValueViewModel> UpdateConfigurationKeyValue(ConfigurationKeyValueViewModel configurationKeyValue);
}