using System.Collections.Generic;

namespace Megarender.Domain
{
    public partial class Organization:Entity
    {
        public string UniqueIdentifier {get;set;}
        public virtual ICollection<UserOrganization> OrganizationUsers {get;set;}
        public virtual List<AccessGroup> AccessGroups {get;set;}
    }
}