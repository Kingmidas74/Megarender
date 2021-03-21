using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Megarender.Business.Exceptions;

namespace Megarender.Business.PipelineBehaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = validators
                .Select(v=>v.Validate(context))
                .SelectMany(x=>x.Errors)
                .Where(x=>x!=null)
                .ToList();
            
            if(failures.Any()) 
                throw new BusinessValidationException(new ValidationException(failures));

            return next();
        }
    }
}