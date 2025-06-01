using Domain.Entities;

namespace Infrastructure.Contracts.TicketSystemServices;

public interface IIncidentCategoryRepository : IRepository<IncidentCategory>
{
   Task<IncidentCategory> GetIncidentCategoryById(int id);
   Task<IEnumerable<IncidentCategory>> GetIncidentCategoriesByOrganizationId(Guid organizationId);
   Task<IEnumerable<IncidentCategory>> GetIncidentCategoriesByOrganizationId(Guid organizationId, int page, int pageSize, string search);
   Task<int> getIncidentCategoryCount (Guid organizationId, string search);
}