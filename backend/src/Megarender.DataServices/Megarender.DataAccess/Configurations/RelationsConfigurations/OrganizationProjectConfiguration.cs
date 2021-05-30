using Megarender.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Megarender.DataAccess
{
    class OrganizationProjectConfiguration : IEntityTypeConfiguration<OrganizationProject>
    {
        public virtual void Configure(EntityTypeBuilder<OrganizationProject> builder)
        {
            builder.Property ($"{nameof(Project)}{nameof(Project.Id)}");
            builder.Property ($"{nameof(Organization)}{nameof(Organization.Id)}");
            builder.HasKey($"{nameof(Project)}{nameof(Project.Id)}", $"{nameof(Organization)}{nameof(Organization.Id)}");
            
            builder.HasOne(c=>c.Project)
                .WithMany(c=>c.ProjectOrganizations)
                .HasForeignKey ($"{nameof(Project)}{nameof(Project.Id)}");
            builder.HasOne(c=>c.Organization)
                .WithMany(c=>c.OrganizationProjects)
                .HasForeignKey ($"{nameof(Organization)}{nameof(Organization.Id)}");  
        }
    }
}