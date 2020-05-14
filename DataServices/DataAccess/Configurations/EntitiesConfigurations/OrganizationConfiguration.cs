using Megarender.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Megarender.DataAccess
{
    class OrganizationConfiguration : UserCreatableConfiguration<Organization>
    {
        public override void Configure(EntityTypeBuilder<Organization> builder)
        {
            base.Configure(builder);
            builder.Property(e=>e.UniqueIdentifier).IsRequired();
            builder.HasIndex(c=>c.UniqueIdentifier)
                .IsUnique();  
        }
    }
}