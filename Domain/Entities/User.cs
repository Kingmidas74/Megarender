using System;
using System.Collections.Generic;

namespace Megarender.Domain
{
    public partial class User:Entity
    {
        public string FirstName {get; set;}
        public string SecondName {get; set;}
        public string SurName {get; set;}
        public DateTime Birthdate {get;set;}
        public virtual ICollection<UserOrganization> UserOrganizations {get;set;} = new HashSet<UserOrganization>();
        public virtual ICollection<AccessGroupUser> UserAcessGroups {get;set;} = new HashSet<AccessGroupUser>();
    }
}