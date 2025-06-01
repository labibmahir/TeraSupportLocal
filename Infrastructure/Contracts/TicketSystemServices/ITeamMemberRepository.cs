using Domain.Entities;

namespace Infrastructure.Contracts.TicketSystemServices;

public interface ITeamMemberRepository : IRepository<TeamMember>
{
    Task<IEnumerable<TeamMember>> GetTeamMembersByOrganizationId(Guid organizationId);
    Task<TeamMember> GetTeamMemberById(int id);
}