using System.Collections.Generic;

namespace Megarender.Domain
{
    public partial class Render:Entity,IUserCreatable
    {
        public string Title {get;set;}        
        public virtual User CreatedBy {get;set;}
        public virtual Scene Scene {get;set;}        
    }
}