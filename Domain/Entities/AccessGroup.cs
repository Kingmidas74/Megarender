using System.Collections.Generic;

namespace Megarender.Domain
{
    public class AccessGroup:Entity, IUserCreatable
    {
        public string Title {get;set;}
        public virtual Organization Organization {get;set;}
        public ICollection<AccessGroupPrivilege> AccessGroupPrivileges {get;set;} = new HashSet<AccessGroupPrivilege>();
        public virtual ICollection<AccessGroupUser> AccessGroupUsers {get;set;}
        public virtual User CreatedBy { get; set; }
    }
}