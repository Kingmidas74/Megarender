using Megarender.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Megarender.DataAccess
{
    class AccessGroupUserConfiguration : IEntityTypeConfiguration<AccessGroupUser>
    {
        public virtual void Configure(EntityTypeBuilder<AccessGroupUser> builder)
        {
            builder.Property ($"{nameof(AccessGroup)}{nameof(AccessGroup.Id)}");
            builder.Property ($"{nameof(User)}{nameof(User.Id)}");
            builder.HasKey($"{nameof(AccessGroup)}{nameof(AccessGroup.Id)}", $"{nameof(User)}{nameof(User.Id)}");

            builder.HasOne<AccessGroup>(c=>c.AccessGroup)
                .WithMany(c=>c.AccessGroupUsers)
                .HasForeignKey ($"{nameof(AccessGroup)}{nameof(AccessGroup.Id)}");
            builder.HasOne<User>(c=>c.User)
                .WithMany(c=>c.UserAcessGroups)
                .HasForeignKey ($"{nameof(User)}{nameof(User.Id)}"); 
        }
    }
}