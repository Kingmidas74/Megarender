using Megarender.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Megarender.DataAccess
{
    class UserOrganizationConfiguration : IEntityTypeConfiguration<UserOrganization>
    {
        public virtual void Configure(EntityTypeBuilder<UserOrganization> builder)
        {
            builder.Property ($"{nameof(Organization)}{nameof(Organization.Id)}");
            builder.Property ($"{nameof(User)}{nameof(User.Id)}");
            builder.HasKey($"{nameof(Organization)}{nameof(Organization.Id)}", $"{nameof(User)}{nameof(User.Id)}");
            
            builder.HasOne<Organization>(c=>c.Organization)
                .WithMany(c=>c.OrganizationUsers)
                .HasForeignKey ($"{nameof(Organization)}{nameof(Organization.Id)}");
            builder.HasOne<User>(c=>c.User)
                .WithMany(c=>c.UserOrganizations)
                .HasForeignKey ($"{nameof(User)}{nameof(User.Id)}");  
        }
    }
}