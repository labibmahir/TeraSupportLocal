using Infrastructure.Contracts.TicketSystemServices;
using Infrastructure.Contracts.UserServices;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();

    IDbContextTransaction BeginTransaction();

    Task<IDbContextTransaction> BeginTransactionAsync();
    
    IUserAccountRepository UserAccountRepository { get; }
  
    IBranchRepository BranchRepository { get; }
    IIncidentCategoryRepository IncidentCategoryRepository { get; }
    IScreenshotRepository ScreenshotRepository { get; }
    IIncidentRepository IncidentRepository { get; }
    ITeamRepository TeamRepository { get; }
    ITeamMemberRepository TeamMemberRepository { get; }
    ITeamLeadRepository TeamLeadRepository { get; }
}