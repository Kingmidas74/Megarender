using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Megarender.Domain;
using Megarender.Domain.Extensions;
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
        public APIContext (DbContextOptions<APIContext> options) : base (options) { }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            modelBuilder.HasDefaultSchema (schema: SchemaName);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(APIContext).Assembly);
            base.OnModelCreating (modelBuilder);
        }

        public override int SaveChanges () {
            AddAuitInfo ();
            return base.SaveChanges ();
        }

        public override async Task<int> SaveChangesAsync (CancellationToken cancellationToken = default) {
            AddAuitInfo ();
            return await base.SaveChangesAsync (cancellationToken);
        }

        private void AddAuitInfo () {
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
            return Database.BeginTransactionAsync(cancellationToken);
        }

        public Task CommitTransactionAsync(CancellationToken cancellationToken = default) 
        {
            return Database.CommitTransactionAsync(cancellationToken);
        }

        public void RollbackTransaction() 
        {
            Database.RollbackTransaction();
        }
    }
}