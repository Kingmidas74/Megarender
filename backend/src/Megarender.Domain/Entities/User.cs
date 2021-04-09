using System;
using System.Collections.Generic;

namespace Megarender.Domain
{
    public partial record User:Entity
    {
        public string FirstName {get;  init;}
        public string SecondName {get;  init;}
        public string SurName {get;  init;}
        public DateTime Birthdate {get; init;}
        public virtual ICollection<UserOrganization> UserOrganizations {get; init;} = new HashSet<UserOrganization>();
        public virtual ICollection<AccessGroupUser> UserAcessGroups {get; init;} = new HashSet<AccessGroupUser>();
        public virtual ICollection<UserProject> UserProjects {get; init;} = new HashSet<UserProject>();
    }
}