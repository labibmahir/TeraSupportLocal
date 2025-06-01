using Domain.Entities;
using Infrastructure.Contracts.TicketSystemServices;

namespace Infrastructure.Repositories.TicketSystemServices;

public class TeamLeadRepository : Repository<TeamLead>, ITeamLeadRepository
{
    private readonly  DataContext context;
    public TeamLeadRepository(DataContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<TeamLead>> GetTeamleadsByOrganizationId(Guid organizationId)
    {
        try
        { 
           return await QueryAsync(teamLead => teamLead.OrganizationId == organizationId && teamLead.IsDeleted == false);   
        }
        catch (Exception ex)
        {
            
            throw new Exception("An error occurred while retrieving team leads.", ex);
        }
    }

    public async Task<TeamLead> GetTeamLeadById(int id)
    {
        return await FirstOrDefaultAsync(t => t.Oid == id && t.IsDeleted == false);
    }
}