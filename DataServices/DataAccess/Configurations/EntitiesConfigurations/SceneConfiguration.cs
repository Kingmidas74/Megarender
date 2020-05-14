using Megarender.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Megarender.DataAccess
{
    class SceneConfiguration : UserCreatableConfiguration<Scene>
    {
        public override void Configure(EntityTypeBuilder<Scene> builder)
        {
            base.Configure(builder);
            builder.Property(e=>e.Title).IsRequired();
            builder.HasOne<Project>(c => c.Project)
                .WithMany (c=>c.Scenes)
                .HasForeignKey ($"{nameof(Project)}{nameof(Project.Id)}")
                .IsRequired();
        }
    }
}