using System.Collections.Generic;

namespace Megarender.Domain
{
    public class AccessGroup:Entity
    {
        public string Title {get;set;}
        public Organization Organization {get;set;}
        public ICollection<AccessGroupPrivilege> AccessGroupPrivileges {get;set;} = new HashSet<AccessGroupPrivilege>();
        public virtual ICollection<AccessGroupUser> AccessGroupUsers {get;set;}
    }
}