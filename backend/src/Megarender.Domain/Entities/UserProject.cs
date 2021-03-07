namespace Megarender.Domain
{
    public record UserProject
    {
        public virtual User User {get; init;}
        public virtual Project Project {get; init;}
    }
}