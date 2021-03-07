namespace Megarender.Domain
{
    public record AccessGroupUser
    {
        public virtual User User {get; init;}
        public virtual AccessGroup AccessGroup {get; init;}
    }
}