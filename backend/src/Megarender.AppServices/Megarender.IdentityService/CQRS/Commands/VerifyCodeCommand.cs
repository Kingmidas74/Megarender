using System;
using MediatR;

namespace Megarender.IdentityService.CQRS
{
    public class VerifyCodeCommand:IRequest<string>
    {
        public Guid Id { get; init; }
        public string Code { get; init; }
    }
}