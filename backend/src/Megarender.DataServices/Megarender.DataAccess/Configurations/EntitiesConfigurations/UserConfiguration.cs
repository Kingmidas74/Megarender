using Megarender.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Megarender.DataAccess
{
    class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.Property(e=>e.FirstName).IsRequired();
            builder.Property(e=>e.SurName).IsRequired();                
            builder.Property(e=>e.Birthdate).IsRequired();
        }
    }
}