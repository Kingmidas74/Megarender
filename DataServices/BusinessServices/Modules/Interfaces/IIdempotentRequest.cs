using System;

namespace Megarender.BusinessServices.Modules
{
    public interface IIdempotentRequest 
    {
        Guid CommandId { get; set; }
    }
}