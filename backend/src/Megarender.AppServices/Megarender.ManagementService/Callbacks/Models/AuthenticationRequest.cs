using System;

namespace Megarender.ManagementService.Callbacks
{
    public record AuthenticationRequest
    {
        public Guid UserId { get; init; }
    }
}