using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Megarender.Domain;
using Microsoft.EntityFrameworkCore;

namespace Megarender.DataAccess {
    public class APIContext : DbContext, IAPIContext {
        private string SchemaName { get; set; } = "public";

        public DbSet<User> Users {get;set;}
        public DbSet<Organization> Organizations {get;set;}
        public DbSet<AccessGroup> AccessGroups {get;set;} 
        public DbSet<Project> Projects {get;set;}       
        public DbSet<Scene> Scenes {get;set;}
        public DbSet<Render> Renders {get;set;}
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
                entity.HasIndex(c=>c.UniqueIdentifier)
                    .IsUnique();
            });

            modelBuilder.Entity<Project> (entity => {
                entity.Property(e=>e.Title).IsRequired();                
            });

            modelBuilder.Entity<Scene> (entity => {
                entity.Property(e=>e.Title).IsRequired();
                entity.HasOne<Project>(c => c.Project)
                    .WithMany (c=>c.Scenes)
                    .HasForeignKey ($"{nameof(Project)}{nameof(Project.Id)}");
            });

            modelBuilder.Entity<Render> (entity => {                
                entity.Property(e=>e.Title).IsRequired();
                entity.HasOne<Scene>(c => c.Scene)
                    .WithMany (c=>c.Renders)
                    .HasForeignKey ($"{nameof(Scene)}{nameof(Scene.Id)}");
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

            modelBuilder.Entity<UserProject> (entity => {
                entity.Property ($"{nameof(Project)}{nameof(Project.Id)}");
                entity.Property ($"{nameof(User)}{nameof(User.Id)}");
                entity.HasKey($"{nameof(Project)}{nameof(Project.Id)}", $"{nameof(User)}{nameof(User.Id)}");
                
                entity.HasOne<Project>(c=>c.Project)
                    .WithMany(c=>c.ProjectUsers)
                    .HasForeignKey ($"{nameof(Project)}{nameof(Project.Id)}");
                entity.HasOne<User>(c=>c.User)
                    .WithMany(c=>c.UserProjects)
                    .HasForeignKey ($"{nameof(User)}{nameof(User.Id)}");                
            });

            modelBuilder.Entity<OrganizationProject> (entity => {
                entity.Property ($"{nameof(Project)}{nameof(Project.Id)}");
                entity.Property ($"{nameof(Organization)}{nameof(Organization.Id)}");
                entity.HasKey($"{nameof(Project)}{nameof(Project.Id)}", $"{nameof(Organization)}{nameof(Organization.Id)}");
                
                entity.HasOne<Project>(c=>c.Project)
                    .WithMany(c=>c.ProjectOrganizations)
                    .HasForeignKey ($"{nameof(Project)}{nameof(Project.Id)}");
                entity.HasOne<Organization>(c=>c.Organization)
                    .WithMany(c=>c.OrganizationProjects)
                    .HasForeignKey ($"{nameof(Organization)}{nameof(Organization.Id)}");                
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