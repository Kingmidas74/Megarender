using System.Collections.Generic;

namespace Megarender.Domain
{
    public partial class Scene:Entity,IUserCreatable
    {
        public string Title {get;set;}        
        public virtual User CreatedBy {get;set;}
        public virtual Project Project {get;set;}        
        public virtual ICollection<Render> Renders {get;set;} = new HashSet<Render>();
    }
}