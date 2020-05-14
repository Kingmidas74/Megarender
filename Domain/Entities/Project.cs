using System.Collections.Generic;

namespace Megarender.Domain
{
    public partial class Project:Entity,IUserCreatable
    {
        public string Title {get;set;}        
        public virtual User CreatedBy {get;set;}
        public virtual Organization Organization {get;set;}
        public virtual ICollection<UserProject> ProjectUsers {get;set;} = new HashSet<UserProject>();
        public virtual ICollection<OrganizationProject> ProjectOrganizations {get;set;} = new HashSet<OrganizationProject>();
        public virtual ICollection<Scene> Scenes {get;set;} = new HashSet<Scene>();
    }
}