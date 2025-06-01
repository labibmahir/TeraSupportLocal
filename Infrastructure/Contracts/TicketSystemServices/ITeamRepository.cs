using Domain.Entities;

namespace Infrastructure.Contracts.TicketSystemServices;

public interface ITeamRepository : IRepository<Team>
{
    Task <Team> GetTeamById(int id);
    Task <IEnumerable<Team>> GetAllTeams();
    Task <IEnumerable<Team>> GetTeamsByOrganizationId(Guid organizationId);
    Task <Team> GetTeamByName(string name, Guid organizationId);
}