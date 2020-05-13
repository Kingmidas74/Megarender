namespace Megarender.Domain
{
    public class AccessGroupPrivilege
    {
        public virtual AccessGroup AccessGroup {get;set;}
        public virtual Privilege Privilege {get;set;}
    }
}