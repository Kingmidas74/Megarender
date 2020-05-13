using System.Collections.Generic;

namespace Megarender.Domain
{
    public class AccessGroup:Entity
    {
        public string Title {get;set;}
        public Organization Organization {get;set;}
        public List<AccessGroupPrivilege> AccessGroupPrivileges {get;set;}
        public virtual ICollection<AccessGroupUser> AccessGroupUsers {get;set;}
    }
}