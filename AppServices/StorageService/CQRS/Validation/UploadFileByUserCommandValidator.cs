using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Validators;
using Megarender.StorageService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Megarender.StorageService.CQRS
{
    public class UploadFileByUserCommandValidator:AbstractValidator<UploadFileByUserCommand>
    {
        private readonly ApplicationOptions Options;
        public UploadFileByUserCommandValidator(IOptions<ApplicationOptions> options)
        {            
            Options = options.Value ?? throw new NullReferenceException(nameof(ApplicationOptions));

            RuleFor(x=>x.Content).NotEmpty();
            RuleFor(x=>x.UserId).NotEmpty();
        }
    }
}