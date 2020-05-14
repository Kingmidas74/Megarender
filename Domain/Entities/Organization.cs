using System.Collections.Generic;

namespace Megarender.Domain
{
    public partial class Organization:Entity,IUserCreatable
    {
        public string UniqueIdentifier {get;set;}
        public virtual ICollection<UserOrganization> OrganizationUsers {get;set;} = new HashSet<UserOrganization>();
        public virtual ICollection<AccessGroup> AccessGroups {get;set;} = new HashSet<AccessGroup>();
        public virtual ICollection<OrganizationProject> OrganizationProjects {get;set;} = new HashSet<OrganizationProject>();
        public virtual User CreatedBy {get;set;}
    }
}