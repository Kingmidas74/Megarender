namespace Megarender.Domain
{
    public record OrganizationProject
    {
        public virtual Project Project {get; init;}
        public virtual Organization Organization {get; init;}
    }
}