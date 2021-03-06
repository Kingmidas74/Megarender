using System;

namespace Megarender.Domain 
{
    public interface IEntity 
    {
        Guid Id { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime ModifiedDate { get; set; }        
        EntityStatusId Status { get; set; }
    }
}