using Domain.Entities;

namespace Infrastructure.Contracts.UserServices;

public interface IUserAccountRepository : IRepository<UserAccount>
{
    public Task<UserAccount> GetUserAccountByKey(Guid id,Guid organisationId);
    
    public Task<UserAccount> GetUserAccountByEmail(string email,Guid organisationId);
    
    public Task<UserAccount> GetUserAccountByKey(Guid key);
    
    public Task<UserAccount> GetUserAccountByCellPhone(string countryCode, string cellPhone,Guid organisationId);
    
    public Task<IEnumerable<UserAccount>> GetUserAccounts(Guid organisationId);
}