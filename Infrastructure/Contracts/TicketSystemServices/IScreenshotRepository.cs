using Domain.Entities;

namespace Infrastructure.Contracts.TicketSystemServices;

public interface IScreenshotRepository : IRepository<Screenshot>
{
    /// <summary>
    /// Returns a Screenshot by its ID.
    /// </summary>
    /// <param name="id">The ID of the screenshot.</param>
    /// <returns>An instance of Screenshot if found, otherwise null.</returns>
    Task<Screenshot> GetScreenshotById(int id);

    /// <summary>
    /// Returns all screenshots associated with a specific incident.
    /// </summary>
    /// <param name="incidentId">The ID of the incident.</param>
    /// <returns>A collection of screenshots related to the incident.</returns>
    Task<IEnumerable<Screenshot>> GetScreenshotsByIncidentId(Guid incidentId);
    
    /// <summary>
    ///  Returns all screenshots in the system.
    /// </summary>
    /// <returns></returns>
    Task <IEnumerable<Screenshot>> GetAllScreenshots();
    
}