using System;
using System.Linq;
using Megarender.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Megarender.DataAccess
{
    class EntityStatusConfiguration : IEntityTypeConfiguration<EntityStatus>
    {
        public virtual void Configure(EntityTypeBuilder<EntityStatus> builder)
        {
            builder.HasData (
                Enum.GetValues (typeof (EntityStatusId))
                .Cast<EntityStatusId> ()
                .Select (e => new EntityStatus
                {
                        EntityStatusId = e,
                        Value = e.ToString ()
                })
            );    
        }
    }
}