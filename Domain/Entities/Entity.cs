using System;

namespace Megarender.Domain
{
    public partial record Entity : IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual EntityStatusId Status { get; set; }
    }
}