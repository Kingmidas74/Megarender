using System.Collections.Generic;

namespace Megarender.Domain
{
    public partial class Organization:Entity
    {
        public string UniqueIdentifier {get;set;}
        public virtual ICollection<UserOrganization> OrganizationUsers {get;set;} = new HashSet<UserOrganization>();
        public virtual ICollection<AccessGroup> AccessGroups {get;set;} = new HashSet<AccessGroup>();
    }
}