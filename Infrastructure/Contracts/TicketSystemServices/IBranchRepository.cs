using Domain.Entities;

namespace Infrastructure.Contracts.TicketSystemServices;

public interface IBranchRepository : IRepository<Branch>
{
    Task <Branch> GetBranchById(int branchId);
    Task <Branch> GetBranchByName(string branchName);
    Task <Branch> DuplicateBranch(string name, Guid organizationId);
    Task<IEnumerable<Branch>> GetBranchesByOrganizationId(Guid organizationId);
    
}