namespace Megarender.Domain
{
    public class UserOrganization
    {
        public virtual User User {get;set;}
        public virtual Organization Organization {get;set;}
    }
}