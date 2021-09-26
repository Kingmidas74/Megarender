using System;
using System.Linq;
using Megarender.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Megarender.DataAccess
{
    class PrivilegeConfiguration : IEntityTypeConfiguration<Privilege>
    {
        public virtual void Configure(EntityTypeBuilder<Privilege> builder)
        {
            builder.HasData (
                Enum.GetValues (typeof (PrivilegeId))
                .Cast<PrivilegeId> ()
                .Select (e => new Privilege
                {
                        PrivilegeId = e,
                        Value = e.ToString ()
                })
            );
        }
    }
}