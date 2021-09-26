using Megarender.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Megarender.DataAccess
{
    class RenderConfiguration : UserCreatableConfiguration<Render>
    {
        public override void Configure(EntityTypeBuilder<Render> builder)
        {
            base.Configure(builder);
            builder.Property(e=>e.Title).IsRequired();
            builder.HasOne(c => c.Scene)
                .WithMany (c=>c.Renders)
                .HasForeignKey ($"{nameof(Scene)}{nameof(Scene.Id)}")
                .IsRequired();
        }
    }
}