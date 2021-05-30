using System.Collections.Generic;

namespace Megarender.Domain
{
    public record AccessGroup:Entity, IUserCreatable
    {
        public string Title {get; init;}
        public virtual Organization Organization {get; init;}
        public virtual ICollection<AccessGroupPrivilege> AccessGroupPrivileges { get; } = new HashSet<AccessGroupPrivilege>();
        public virtual ICollection<AccessGroupUser> AccessGroupUsers {get; init;}
        public virtual User CreatedBy { get; set; }
    }
}