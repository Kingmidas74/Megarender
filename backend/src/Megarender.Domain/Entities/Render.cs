namespace Megarender.Domain
{
    public record Render:Entity,IUserCreatable
    {
        public string Title {get; init;}        
        public virtual User CreatedBy {get; init;}
        public virtual Scene Scene {get; init;}        
    }
}