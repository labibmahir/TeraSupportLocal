using Domain.Entities;
using Infrastructure.Contracts.TicketSystemServices;

namespace Infrastructure.Repositories.TicketSystemServices;

public class ScreenshotRepository : Repository<Screenshot>,  IScreenshotRepository
{
     public ScreenshotRepository (DataContext context) : base(context)
     {
     }

     public async Task<Screenshot> GetScreenshotById(int id)
     {
          try
          {
               return await FirstOrDefaultAsync(s => s.Oid == id && s.IsDeleted == false);
          }
          catch (Exception)
          {
               throw;
          }
     }

     public async Task<IEnumerable<Screenshot>> GetScreenshotsByIncidentId(Guid incidentId)
     {
          try
          {
               return await QueryAsync(s => s.IncidentId == incidentId && s.IsDeleted == false);
          }
          catch (Exception)
          {
               throw;
          }
     }

     public Task<IEnumerable<Screenshot>> GetAllScreenshots()
     {
          try
          {
               return QueryAsync(s => s.IsDeleted == false, o => o.Oid);
          }
          catch (Exception)
          {
               throw;
          }
     }
}