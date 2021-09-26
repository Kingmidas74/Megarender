using System.Collections.Generic;
using System.Linq;

namespace Megarender.Domain
{
    public record AccessGroup:Entity, IUserCreatable
    {
        public string Title { get; init; }
        public virtual Organization Organization { get; init; }
        public virtual ICollection<AccessGroupPrivilege> AccessGroupPrivileges { get; } = new HashSet<AccessGroupPrivilege>();
        public virtual ICollection<AccessGroupUser> AccessGroupUsers { get; init; }
        public virtual User CreatedBy { get; set; }
        public PrivilegeId Privilege => (PrivilegeId) AccessGroupPrivileges.Select(agp => agp.Privilege)
            .Aggregate(0, (acc, value) => acc | (int) value.PrivilegeId);
    }
}