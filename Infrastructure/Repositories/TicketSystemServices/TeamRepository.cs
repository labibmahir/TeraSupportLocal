using Domain.Entities;
using Infrastructure.Contracts.TicketSystemServices;

namespace Infrastructure.Repositories.TicketSystemServices;

public class TeamRepository : Repository<Team>, ITeamRepository
{
    private readonly DataContext context;
    public TeamRepository (DataContext context) : base(context)
    {
        this.context = context;
    }

    public Task<Team> GetTeamById(int id)
    {
        try
        {
            return FirstOrDefaultAsync(t => t.Oid == id && t.IsDeleted == false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<IEnumerable<Team>> GetAllTeams()
    {
        try
        {
            return QueryAsync(t => t.IsDeleted == false, o => o.Oid);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<IEnumerable<Team>> GetTeamsByOrganizationId(Guid organizationId)
    {
        try
        {
            return QueryAsync(t => t.OrganizationId == organizationId && t.IsDeleted == false, o => o.Oid);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public Task<Team> GetTeamByName(string name, Guid organizationId)
    {
        try
        {
            return FirstOrDefaultAsync(t => t.Name.Trim().ToLower() == name.Trim().ToLower() && t.OrganizationId == organizationId && t.IsDeleted == false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}