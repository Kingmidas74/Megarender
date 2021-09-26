using System;

namespace Megarender.Domain
{
    public record Entity : IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual EntityStatusId Status { get; set; }
    }
}