using Domain.Entities;
using Infrastructure.Contracts.TicketSystemServices;
using Infrastructure.Contracts.UserServices;
using Infrastructure.Repositories.UserServices;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Infrastructure.Repositories.TicketSystemServices;

namespace Infrastructure;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    protected readonly DataContext context;
    protected readonly IConfiguration configuration;
    private bool disposed;

    public UnitOfWork(DataContext context, IConfiguration configuration)
    {
        this.context = context;
        this.configuration = configuration;
    }

    #region Configuration
    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }

    public IDbContextTransaction BeginTransaction()
    {
        return context.Database.BeginTransaction();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await context.Database.BeginTransactionAsync();
    }

    protected void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }

        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    #endregion
    
    #region UserAccount
    private IUserAccountRepository userAccountRepository;
    public IUserAccountRepository UserAccountRepository
    {
        get
        {
            if (userAccountRepository == null)
                userAccountRepository = new UserAccountRepository(context);

            return userAccountRepository;
        }
    }
    
    #endregion
    
     #region Branch
        private IBranchRepository branchRepository;

        public IBranchRepository BranchRepository
        {
            get
            {
                if (branchRepository == null)
                    branchRepository = new BranchRepository(context);

                return branchRepository;
            }
        }
     #endregion

    #region IncidentCategory

    private IIncidentCategoryRepository incidentCategoryRepository;

    public IIncidentCategoryRepository IncidentCategoryRepository
    {
        get
        {
            if (incidentCategoryRepository == null)
                incidentCategoryRepository = new IncidentCategoryRepository(context);

            return incidentCategoryRepository;
        }
    }

    #endregion
    
    #region Screenshot
    private IScreenshotRepository screenshotRepository;
    public IScreenshotRepository ScreenshotRepository
    {
        get
        {
            if (screenshotRepository == null)
                screenshotRepository = new ScreenshotRepository(context);

            return screenshotRepository;
        }
    }
    #endregion
    
    #region Incident
    private IIncidentRepository incidentRepository;
    public IIncidentRepository IncidentRepository
    {
        get
        {
            if (incidentRepository == null)
                incidentRepository = new IncidentRepository(context);

            return incidentRepository;
        }
    }
    #endregion

    #region  Team
    private ITeamRepository teamRepository;
    public ITeamRepository TeamRepository
    {
        get
        {
            if (teamRepository == null)
                teamRepository = new TeamRepository(context);

            return teamRepository;
        }
    }
    #endregion
    
    #region TeamMember
    private ITeamMemberRepository teamMemberRepository;
    public ITeamMemberRepository TeamMemberRepository
    {
        get
        {
            if (teamMemberRepository == null)
                teamMemberRepository = new TeamMemberRepository(context);

            return teamMemberRepository;
        }
    }
    #endregion
    
    #region TeamLead
    private ITeamLeadRepository teamLeadRepository;
    public ITeamLeadRepository TeamLeadRepository
    {
        get
        {
            if (teamLeadRepository == null)
                teamLeadRepository = new TeamLeadRepository(context);

            return teamLeadRepository;
        }
    }
    #endregion
}