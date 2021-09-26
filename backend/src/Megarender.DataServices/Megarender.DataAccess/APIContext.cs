using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Megarender.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Megarender.DataAccess {
    public class APIContext : DbContext, IAPIContext {
        private string SchemaName { get; set; } = "public";

        public DbSet<User> Users {get;set;}
        public DbSet<Organization> Organizations {get;set;}
        public DbSet<AccessGroup> AccessGroups {get;set;} 
        public DbSet<Project> Projects {get;set;}       
        public DbSet<Scene> Scenes {get;set;}
        public DbSet<Render> Renders {get;set;}
        public DbSet<SharedMoneyTransaction> SharedMoneyTransactions {get;set;}
        public DbSet<PrivateMoneyTransaction> PrivateMoneyTransactions {get;set;}
        
        public DbSet<UserOrganization> UserOrganizations { get; set; }
        public APIContext (DbContextOptions<APIContext> options) : base (options) { }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            modelBuilder.HasDefaultSchema (schema: SchemaName);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(APIContext).Assembly);
            base.OnModelCreating (modelBuilder);
        }

        public override int SaveChanges () {
            AddAuditInfo ();
            return base.SaveChanges ();
        }

        public override async Task<int> SaveChangesAsync (CancellationToken cancellationToken = default) {
            AddAuditInfo ();
            return await base.SaveChangesAsync (cancellationToken);
        }

        private void AddAuditInfo () {
            var entries = ChangeTracker.Entries ().Where (x => x.Entity is IEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entry in entries) {
                if (entry.State == EntityState.Added) {
                    ((IEntity) entry.Entity).CreatedDate = DateTime.UtcNow;
                    ((IEntity) entry.Entity).Status = EntityStatusId.Active;
                }
                ((IEntity) entry.Entity).ModifiedDate = DateTime.UtcNow;
            }
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default) 
        {
            return Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default)
        {
            await SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }

        public void RollbackTransaction(IDbContextTransaction transaction) 
        {
            try
            {
                transaction?.Rollback();
            }
            finally
            {
                if (transaction != null)
                {
                    transaction.Dispose();
                    transaction = null;
                }
            }
        }
    }
}