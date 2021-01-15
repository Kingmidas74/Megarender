namespace Megarender.Domain
{
    public record AccessGroupPrivilege
    {
        public virtual AccessGroup AccessGroup {get; init;}
        public virtual Privilege Privilege {get; init;}
    }
}