using FluentValidation;
using MediatR;
using Megarender.Features.PipelineBehaviors;
using Microsoft.Extensions.DependencyInjection;

namespace Megarender.Features
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DependencyInjection));
            services.AddMediatR(typeof(DependencyInjection));
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);    
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(IdempotentBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }    
}
