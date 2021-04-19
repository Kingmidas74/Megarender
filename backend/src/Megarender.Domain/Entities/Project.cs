using System.Collections.Generic;

namespace Megarender.Domain
{
    public record Project:Entity,IUserCreatable
    {
        public string Title {get; init;}        
        public virtual User CreatedBy {get; init;}
        public virtual Organization Organization {get; init;}
        public virtual ICollection<UserProject> ProjectUsers {get; init;} = new HashSet<UserProject>();
        public virtual ICollection<OrganizationProject> ProjectOrganizations {get; init;} = new HashSet<OrganizationProject>();
        public virtual ICollection<Scene> Scenes {get; init;} = new HashSet<Scene>();
    }
}