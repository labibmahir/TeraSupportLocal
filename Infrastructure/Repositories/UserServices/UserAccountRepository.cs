using Domain.Entities;
using Infrastructure.Contracts.UserServices;

namespace Infrastructure.Repositories.UserServices;

public class UserAccountRepository : Repository<UserAccount>, IUserAccountRepository
{
    public UserAccountRepository(DataContext context) : base(context)
    {
        
    }

    public async Task<UserAccount> GetUserAccountByKey(Guid id,Guid organisationId)
    {
        try
        {
            return await FirstOrDefaultAsync(u => u.Oid == id && u.OrganizationId == organisationId && u.IsDeleted == false);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<UserAccount> GetUserAccountByEmail(string email,Guid organisationId)
    {
        try
        {
            return await FirstOrDefaultAsync(u => u.Email.Trim() == email.Trim() && u.OrganizationId == organisationId && u.IsDeleted == false);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public Task<UserAccount> GetUserAccountByKey(Guid key)
    {
        try
        {
            return FirstOrDefaultAsync(u => u.Oid == key && u.IsDeleted == false);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<UserAccount> GetUserAccountByCellPhone(string countryCode, string cellPhone,Guid organisationId)
    {
        try
        {
            return await FirstOrDefaultAsync(u => u.CountryCode == countryCode && u.Cellphone == cellPhone && u.OrganizationId == organisationId && u.IsDeleted == false);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<IEnumerable<UserAccount>> GetUserAccounts(Guid organisationId)
    {
        try
        {
            return await QueryAsync(u => u.OrganizationId == organisationId && u.IsDeleted == false);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}