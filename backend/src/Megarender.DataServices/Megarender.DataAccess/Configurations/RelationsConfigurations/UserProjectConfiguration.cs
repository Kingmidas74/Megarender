using Megarender.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Megarender.DataAccess
{
    class UserProjectConfiguration : IEntityTypeConfiguration<UserProject>
    {
        public virtual void Configure(EntityTypeBuilder<UserProject> builder)
        {
            builder.Property ($"{nameof(Project)}{nameof(Project.Id)}");
            builder.Property ($"{nameof(User)}{nameof(User.Id)}");
            builder.HasKey($"{nameof(Project)}{nameof(Project.Id)}", $"{nameof(User)}{nameof(User.Id)}");
            
            builder.HasOne(c=>c.Project)
                .WithMany(c=>c.ProjectUsers)
                .HasForeignKey ($"{nameof(Project)}{nameof(Project.Id)}");
            builder.HasOne(c=>c.User)
                .WithMany(c=>c.UserProjects)
                .HasForeignKey ($"{nameof(User)}{nameof(User.Id)}");     
        }
    }
}