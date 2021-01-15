using System.Collections.Generic;

namespace Megarender.Domain
{
    public partial record Organization:Entity,IUserCreatable
    {
        public string UniqueIdentifier {get; init;}
        public virtual ICollection<UserOrganization> OrganizationUsers {get; init;} = new HashSet<UserOrganization>();
        public virtual ICollection<AccessGroup> AccessGroups {get; init;} = new HashSet<AccessGroup>();
        public virtual ICollection<OrganizationProject> OrganizationProjects {get; init;} = new HashSet<OrganizationProject>();
        public virtual User CreatedBy {get; init;}
    }
}