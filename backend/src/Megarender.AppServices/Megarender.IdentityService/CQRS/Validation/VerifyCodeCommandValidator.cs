using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Megarender.IdentityService.CQRS
{
    public class VerifyCodeCommandValidator:AbstractValidator<VerifyCodeCommand>
    {
        public VerifyCodeCommandValidator()
        {
            RuleFor(x=>x.Phone).NotEmpty();
            RuleFor(x=>x.Code).NotEmpty();
            //RuleFor(x => x.ServiceWebHook).NotEmpty();
        }
    }
}