using Domain.Entities;
using Infrastructure.Contracts.TicketSystemServices;

namespace Infrastructure.Repositories.TicketSystemServices;

public class BranchRepository : Repository<Domain.Entities.Branch>, IBranchRepository
{
    private readonly DataContext context;

    public BranchRepository(DataContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<Branch> GetBranchById(int branchId)
    {
        try
        {
            return await FirstOrDefaultAsync(b => b.Oid == branchId);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving branch by ID {branchId}: {ex.Message}", ex);
        }
    }

    public Task<Branch> GetBranchByName(string branchName)
    {
        try
        {
            return FirstOrDefaultAsync(b => b.Name.Trim().ToLower() == branchName.Trim().ToLower());

        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving branch by name {branchName}: {ex.Message}", ex);
        }
    }

    public Task<Branch> DuplicateBranch(string name, Guid organizationId)
    {
        try
        {
            return FirstOrDefaultAsync(b => b.Name.Trim().ToLower() == name.Trim().ToLower() && b.OrganizationId == organizationId);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error duplicating branch with name {name} in organization {organizationId}: {ex.Message}", ex);
        }
    }
    
    public async Task<IEnumerable<Branch>> GetBranchesByOrganizationId(Guid organizationId)
    {
        try
        {
            var branchesInDb = await QueryAsync(b => b.OrganizationId == organizationId && b.IsDeleted == false);
            if (branchesInDb == null || !branchesInDb.Any())
            {
                throw new Exception($"No branches found for organization ID {organizationId}");
            }
            return branchesInDb;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving branches for organization ID {organizationId}: {ex.Message}", ex);
        }
    }
}