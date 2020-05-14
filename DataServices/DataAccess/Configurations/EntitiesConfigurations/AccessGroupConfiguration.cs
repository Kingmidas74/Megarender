using Megarender.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Megarender.DataAccess
{
    class AccessGroupConfiguration : UserCreatableConfiguration<AccessGroup>
    {
        public override void Configure(EntityTypeBuilder<AccessGroup> builder)
        {
            base.Configure(builder);
            builder.Property ($"{nameof(Organization)}{nameof(Organization.Id)}");            
            builder.HasOne<Organization>(c => c.Organization)
                .WithMany (c => c.AccessGroups)
                .HasForeignKey ($"{nameof(Organization)}{nameof(Organization.Id)}")
                .IsRequired();
        }
    }
}