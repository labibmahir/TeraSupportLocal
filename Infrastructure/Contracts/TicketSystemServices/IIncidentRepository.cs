using Domain.Entities;
using Infrastructure.Migrations;

namespace Infrastructure.Contracts.TicketSystemServices;

public interface IIncidentRepository : IRepository<Incident>
{
    Task<Incident> GetIncidentById(Guid id);
    Task<IEnumerable<Incident>> GetIncidentbyOrganizationId(Guid organizationId);
    Task<IEnumerable<Incident>> GetIncidentbyOrganizationId(Guid organizationId, int page, int pageSize, string search);
 }
