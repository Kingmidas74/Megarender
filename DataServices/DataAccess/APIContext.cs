using System;
using System.Linq;
using System.Threading.Tasks;
using Megarender.Domain;
using Microsoft.EntityFrameworkCore;

namespace Megarender.DataAccess {
    public class APIContext : DbContext,IAPIContext {
        private string SchemaName { get; set; } = "public";

        public APIContext (DbContextOptions<APIContext> options) : base (options) { }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            modelBuilder.HasDefaultSchema (schema: SchemaName);

            modelBuilder.Entity<EntityStatus> (entity => {
                entity.HasData (
                    Enum.GetValues (typeof (EntityStatusId))
                    .Cast<EntityStatusId> ()
                    .Select (e => new EntityStatus () {
                        EntityStatusId = e,
                            Value = e.ToString ()
                    })
                );
            });

            base.OnModelCreating (modelBuilder);
        }

        public override int SaveChanges () {
            AddAuitInfo ();
            return base.SaveChanges ();
        }

        public async Task<int> SaveChangesAsync () {
            AddAuitInfo ();
            return await base.SaveChangesAsync ();
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
    }
}