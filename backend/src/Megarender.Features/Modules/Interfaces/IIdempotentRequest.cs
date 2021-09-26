using System;

namespace Megarender.Features.Modules
{
    public interface IIdempotentRequest 
    {
        Guid CommandId { get; set; }
    }
}