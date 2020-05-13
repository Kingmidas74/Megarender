namespace Megarender.Domain
{
    public class AccessGroupUser
    {
        public virtual User User {get;set;}
        public virtual AccessGroup AccessGroup {get;set;}
    }
}