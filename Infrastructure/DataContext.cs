using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }
    
    public DbSet<UserAccount> UserAccounts { get; set; }
    
    public DbSet<Biometric> Biometrics { get; set; }
    
    public DbSet<Branch> Branches { get; set; }
    
    public DbSet<IncidentCategory> IncidentCategories { get; set; } 
    
    public DbSet<Incident> Incidents { get; set; }
    
    public DbSet<Message> Messages { get; set; }
    
    public DbSet<MonitorNetworkDeviceConfiguration> MonitorNetworkDeviceConfigurations { get; set; }
    
    public DbSet<NetworkDevice> NetworkDevices { get; set; }
    
    public DbSet<Screenshot>Screenshots { get; set; }
    
    public DbSet<Team> Teams { get; set; }
    
    public DbSet<TeamMember> TeamMembers { get; set; }
    
    public DbSet<TeamLead> TeamLeads { get; set; }
}