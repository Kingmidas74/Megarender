namespace Megarender.Domain
{
    public class OrganizationProject
    {
        public virtual Project Project {get;set;}
        public virtual Organization Organization {get;set;}
    }
}