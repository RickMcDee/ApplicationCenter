namespace ApplicationCenter.Api.Services;

public interface IApplicationService
{
    /// <summary>
    /// Adds or updates an Application to the database.
    /// </summary>
    /// <remarks>
    /// New Applications that should be added to the database should have a Guid.Empty-Id
    /// </remarks>
    /// <param name="application">The model to add or update</param>
    /// <returns>The updated model</returns>
    /// <exception cref="KeyNotFoundException">There is no Application with the given id</exception>
    Task<ApplicationViewModel> AddOrUpdateApplication(ApplicationViewModel application);
    
    /// <summary>
    /// Returns a list of all Applications.
    /// Applications will include ConfigurationKey.
    /// </summary>
    /// <returns>A list of all Applications</returns>
    Task<IEnumerable<ApplicationViewModel>> GetApplications();
    
    /// <summary>
    /// Removes an Application from the database.
    /// This will also remove the ConfigurationKeys for this Application.
    /// </summary>
    /// <param name="applicationId">The id of the Application that should be removed</param>
    /// <exception cref="KeyNotFoundException">There is no Application with the given id</exception>
    Task RemoveApplication(Guid applicationId);
}
