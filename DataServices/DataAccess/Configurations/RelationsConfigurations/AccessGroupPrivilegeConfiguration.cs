using Megarender.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Megarender.DataAccess
{
    class AccessGroupPrivilegeConfiguration : IEntityTypeConfiguration<AccessGroupPrivilege>
    {
        public virtual void Configure(EntityTypeBuilder<AccessGroupPrivilege> builder)
        {
            builder.Property ($"{nameof(AccessGroup)}{nameof(AccessGroup.Id)}");
            builder.Property ($"{nameof(PrivilegeId)}");
            builder.HasKey($"{nameof(AccessGroup)}{nameof(AccessGroup.Id)}", $"{nameof(PrivilegeId)}");

            builder.HasOne<AccessGroup>(c=>c.AccessGroup)
                .WithMany(c=>c.AccessGroupPrivileges)
                .HasForeignKey ($"{nameof(AccessGroup)}{nameof(AccessGroup.Id)}");
            builder.HasOne<Privilege>(c=>c.Privilege)
                .WithMany()
                .HasForeignKey ($"{nameof(PrivilegeId)}");
        }
    }
}