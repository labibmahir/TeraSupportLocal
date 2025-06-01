using Domain.Entities;
using Infrastructure.Contracts.TicketSystemServices;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.TicketSystemServices;

public class IncidentRepository : Repository<Incident>, IIncidentRepository
{
    private readonly DataContext context;
    public IncidentRepository(DataContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<Incident> GetIncidentById(Guid id)
    {
        try
        {
            return await FirstOrDefaultAsync(i => i.Oid == id && i.IsDeleted == false);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<Incident>> GetIncidentbyOrganizationId(Guid organizationId)
    {
        try
        {
            return await QueryAsync(i => i.OrganizationId == organizationId && i.IsDeleted == false);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<IEnumerable<Incident>> GetIncidentbyOrganizationId(Guid organizationId, int page, int pageSize, string search)
    {
        try
        {
            IQueryable<Incident> query = context.Incidents.Where(i => i.OrganizationId == organizationId && i.IsDeleted == false);
            
            if(!string.IsNullOrEmpty(search))
            {
                query = query.Where(i => i.TicketTitle.Contains(search) || i.Description.Contains(search));
            }
            query = query.OrderByDescending(ic => ic.DateCreated).Skip((page - 1) * pageSize).Take(pageSize);
            
            return await query.ToListAsync();

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    
}