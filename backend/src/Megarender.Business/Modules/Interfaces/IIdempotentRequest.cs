using System;

namespace Megarender.Business.Modules
{
    public interface IIdempotentRequest 
    {
        Guid CommandId { get; init; }
    }
}