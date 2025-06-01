using Domain.Entities;

namespace Infrastructure.Contracts.TicketSystemServices;

public interface ITeamLeadRepository : IRepository<TeamLead>
{
    Task <IEnumerable<TeamLead>> GetTeamleadsByOrganizationId(Guid organizationId);
    Task<TeamLead> GetTeamLeadById(int id);
}