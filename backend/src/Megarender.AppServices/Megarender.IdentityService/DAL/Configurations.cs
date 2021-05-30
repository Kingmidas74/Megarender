using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Megarender.IdentityService
{
    class EntityStatusConfiguration : IEntityTypeConfiguration<CommunicationChannel>
    {
        public virtual void Configure(EntityTypeBuilder<CommunicationChannel> builder)
        {
            builder.HasData (
                Enum.GetValues (typeof (CommunicationChannelId))
                    .Cast<CommunicationChannelId> ()
                    .Select (e => new CommunicationChannel
                    {
                        CommunicationChannelId = e,
                        Value = e.ToString ()
                    })
            );    
        }
    }
    
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e=>e.Id);
            builder.Property(e=>e.PreferredCommunicationChannel)
                .HasConversion<int> ();
            builder.OwnsOne(u => u.CommunicationChannelsData, ownedBuilder =>
            {
                ownedBuilder.WithOwner()
                    .HasForeignKey($"{nameof(User)}{nameof(User.Id)}");
                ownedBuilder.ToTable(nameof(CommunicationChannelsData));
            });
        }
    }
}