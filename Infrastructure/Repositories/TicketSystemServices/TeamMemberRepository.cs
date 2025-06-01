using Domain.Entities;
using Infrastructure.Contracts.TicketSystemServices;

namespace Infrastructure.Repositories.TicketSystemServices;

public class TeamMemberRepository : Repository<TeamMember>, ITeamMemberRepository
{
    private readonly DataContext context;
    public TeamMemberRepository(DataContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<TeamMember>> GetTeamMembersByOrganizationId(Guid organizationId)
    {
        try
        {
            return await QueryAsync(teamMember => teamMember.OrganizationId == organizationId && teamMember.IsDeleted == false);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving team members.", ex);
        }
    }

    public async Task<TeamMember> GetTeamMemberById(int id)
    {
        try
        {
            return await FirstOrDefaultAsync(t => t.Oid == id && t.IsDeleted == false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}