using System.Collections.Generic;
using System.Linq;

namespace Megarender.Domain
{
    public record User:Entity
    {
        public virtual ICollection<UserOrganization> UserOrganizations {get; init;} = new HashSet<UserOrganization>();
        public virtual ICollection<AccessGroupUser> UserAcessGroups {get; init;} = new HashSet<AccessGroupUser>();
        public virtual ICollection<UserProject> UserProjects {get; init;} = new HashSet<UserProject>();
        
        public PrivilegeId Privilege => UserAcessGroups.Select(uag=>uag.Privilege)
    }
}