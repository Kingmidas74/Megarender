using System.Collections.Generic;

namespace Megarender.Domain
{
    public partial record Scene:Entity,IUserCreatable
    {
        public string Title {get; init;}        
        public virtual User CreatedBy {get; init;}
        public virtual Project Project {get; init;}        
        public virtual ICollection<Render> Renders {get; init;} = new HashSet<Render>();
    }
}