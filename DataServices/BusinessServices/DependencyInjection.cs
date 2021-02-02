using AutoMapper;
using Megarender.BusinessServices.PipelineBehaviors;
using Megarender.BusinessServices.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Megarender.BusinessServices
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DependencyInjection));
            services.AddAutoMapper (typeof (DependencyInjection));
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);    
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(IdempotentBehaviour<,>));
            return services;
        }
    }    
}
