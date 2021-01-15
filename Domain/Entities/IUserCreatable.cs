namespace Megarender.Domain
{
    public interface IUserCreatable:IEntity
    {
        User CreatedBy {get;}
    }
}