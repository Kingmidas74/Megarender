using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Megarender.Domain;
using Microsoft.EntityFrameworkCore;

namespace Megarender.DataAccess {
    public class APIContext : DbContext,IAPIContext {
        private string SchemaName { get; set; } = "public";

        public DbSet<User> Users {get;set;}
        public DbSet<Organization> Organizations {get;set;}
        public DbSet<AccessGroup> AccessGroups {get;set;}        
        public APIContext (DbContextOptions<APIContext> options) : base (options) { }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            modelBuilder.HasDefaultSchema (schema: SchemaName);

            //modelBuilder.HasPostgresEnum<PrivilegeId>();

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

            modelBuilder.Entity<Privilege> (entity => {
                entity.HasData (
                    Enum.GetValues (typeof (PrivilegeId))
                    .Cast<PrivilegeId> ()
                    .Select (e => new Privilege () {
                            PrivilegeId = e,
                            Value = e.ToString ()
                    })
                );
            });

            modelBuilder.Entity<User> (entity => {
                entity.Property (e => e.Status)
                    .HasConversion<int> ();
            });

            modelBuilder.Entity<Organization> (entity => {
                entity.Property (e => e.Status)
                    .HasConversion<int> ();
            });

            modelBuilder.Entity<AccessGroup> (entity => {
                entity.Property ($"{nameof(Organization)}{nameof(Organization.Id)}");
                entity.Property (e => e.Status)
                    .HasConversion<int> ();
                entity.HasOne<Organization>(c => c.Organization)
                    .WithMany (c => c.AccessGroups)
                    .HasForeignKey ($"{nameof(Organization)}{nameof(Organization.Id)}");
            });

            modelBuilder.Entity<AccessGroupPrivilege> (entity => {
                entity.Property ($"{nameof(AccessGroup)}{nameof(AccessGroup.Id)}");
                entity.Property ($"{nameof(PrivilegeId)}");
                entity.HasKey($"{nameof(AccessGroup)}{nameof(AccessGroup.Id)}", $"{nameof(PrivilegeId)}");

                entity.HasOne<AccessGroup>(c=>c.AccessGroup)
                    .WithMany(c=>c.AccessGroupPrivileges)
                    .HasForeignKey ($"{nameof(AccessGroup)}{nameof(AccessGroup.Id)}");
                entity.HasOne<Privilege>(c=>c.Privilege)
                    .WithMany()
                    .HasForeignKey ($"{nameof(PrivilegeId)}");  
            });

            modelBuilder.Entity<UserOrganization> (entity => {
                entity.Property ($"{nameof(Organization)}{nameof(Organization.Id)}");
                entity.Property ($"{nameof(User)}{nameof(User.Id)}");
                entity.HasKey($"{nameof(Organization)}{nameof(Organization.Id)}", $"{nameof(User)}{nameof(User.Id)}");
                
                entity.HasOne<Organization>(c=>c.Organization)
                    .WithMany(c=>c.OrganizationUsers)
                    .HasForeignKey ($"{nameof(Organization)}{nameof(Organization.Id)}");
                entity.HasOne<User>(c=>c.User)
                    .WithMany(c=>c.UserOrganizations)
                    .HasForeignKey ($"{nameof(User)}{nameof(User.Id)}");                
            });

            modelBuilder.Entity<AccessGroupUser> (entity => {
                entity.Property ($"{nameof(AccessGroup)}{nameof(AccessGroup.Id)}");
                entity.Property ($"{nameof(User)}{nameof(User.Id)}");
                entity.HasKey($"{nameof(AccessGroup)}{nameof(AccessGroup.Id)}", $"{nameof(User)}{nameof(User.Id)}");

                entity.HasOne<AccessGroup>(c=>c.AccessGroup)
                    .WithMany(c=>c.AccessGroupUsers)
                    .HasForeignKey ($"{nameof(AccessGroup)}{nameof(AccessGroup.Id)}");
                entity.HasOne<User>(c=>c.User)
                    .WithMany(c=>c.UserAcessGroups)
                    .HasForeignKey ($"{nameof(User)}{nameof(User.Id)}");                
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