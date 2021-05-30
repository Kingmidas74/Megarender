using System.Threading;
using System.Threading.Tasks;
using Megarender.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Megarender.DataAccess
{
    public interface IAPIContext 
    {
        DbSet<User> Users {get;set;}
        DbSet<Organization> Organizations {get;set;}
        DbSet<AccessGroup> AccessGroups {get;set;}
        DbSet<Project> Projects {get;set;}       
        DbSet<Scene> Scenes {get;set;}
        DbSet<Render> Renders {get;set;}
        DbSet<SharedMoneyTransaction> SharedMoneyTransactions {get;set;}
        DbSet<PrivateMoneyTransaction> PrivateMoneyTransactions {get;set;}        
        
        DbSet<UserOrganization> UserOrganizations { get; set; }
        
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default);
        void RollbackTransaction(IDbContextTransaction transaction);
    }
}