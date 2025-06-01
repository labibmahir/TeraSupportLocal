using Domain.Entities;
using Infrastructure.Contracts.TicketSystemServices;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.TicketSystemServices;

public class IncidentCategoryRepository : Repository<IncidentCategory>, IIncidentCategoryRepository
{
    private readonly DataContext context;
        public IncidentCategoryRepository(DataContext context) : base(context)
    {
        this.context = context;
    }


    public Task<IncidentCategory> GetIncidentCategoryById(int id)
    {
        try
        {
            return FirstOrDefaultAsync(ic => ic.Oid == id && ic.IsDeleted == false);
        }
        catch (Exception)
        {
            throw;  
        }
    }

    public async Task<IEnumerable<IncidentCategory>> GetIncidentCategoriesByOrganizationId(Guid organizationId)
    {
        try
        {
            return await  context.IncidentCategories
                .Where(ic => ic.OrganizationId == organizationId && ic.IsDeleted == false)
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IEnumerable<IncidentCategory>> GetIncidentCategoriesByOrganizationId(Guid organizationId, int page, int pageSize, string search)
    {
        try
        {
            IQueryable<IncidentCategory> query = context.IncidentCategories.Where( c=> c.OrganizationId == organizationId && c.IsDeleted == false);
            
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(ic => ic.Description.Contains(search) || ic.Description.Contains(search));
            }

            query = query.OrderByDescending(ic => ic.DateCreated).Skip((page - 1) * pageSize).Take(pageSize);
            return await query.ToListAsync();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<int> getIncidentCategoryCount(Guid organizationId, string search)
    {
        try
        {
            IQueryable<IncidentCategory> query = context.IncidentCategories.Where(ic => ic.OrganizationId == organizationId && ic.IsDeleted == false);
            
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(ic => ic.Description.Contains(search) || ic.Description.Contains(search));
            }

            return await query.CountAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
