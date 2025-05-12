namespace ApplicationCenter.Api.Services;

public interface IConfigurationKeyService
{
    /// <summary>
    /// Adds or updates a ConfigurationKey to the database. This also includes creating new ConfigurationKeyValues.
    /// </summary>
    /// <remarks>
    /// New ConfigurationKeys that should be added to the database should have a Guid.Empty-Id
    /// </remarks>
    /// <remarks>
    /// The Update-Path of this method does NOT update ConfigurationKeyValues
    /// </remarks>
    /// <param name="configurationKey">The model to add or update</param>
    /// <param name="applicationId">The id of the application where the ConfigurationKey is added(optional - Only used for adding new ConfigurationKeys)</param>
    /// <returns>The updated model</returns>
    /// <exception cref="KeyNotFoundException">There is no ConfigurationKey with the given id</exception>
    Task<ConfigurationKeyViewModel> AddOrUpdateConfigurationKey(ConfigurationKeyViewModel configurationKey, Guid? applicationId);
    
    /// <summary>
    /// Returns a list of ConfigurationKeys for the given application.
    /// ConfigurationKeys will include ConfigurationKeyValues.
    /// </summary>
    /// <param name="applicationId">The id of the application you want the list for</param>
    /// <returns>A list of ConfigurationKeys for the given application</returns>
    Task<IEnumerable<ConfigurationKeyViewModel>> GetConfigurationKeys(Guid applicationId);
    
    /// <summary>
    /// Removes a ConfigurationKey from the database.
    /// This will also remove the ConfigurationKeyValues for this ConfigurationKey.
    /// </summary>
    /// <param name="configurationKeyId">The id of the ConfigurationKey that should be removed</param>
    /// <exception cref="KeyNotFoundException">There is no ConfigurationKey with the given id</exception>
    Task RemoveConfigurationKey(Guid configurationKeyId);
}
