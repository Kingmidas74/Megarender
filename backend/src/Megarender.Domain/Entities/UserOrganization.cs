namespace Megarender.Domain
{
    public record UserOrganization
    {
        public virtual User User {get; init;}
        public virtual Organization Organization {get; init;}
    }
}