using Microsoft.Extensions.DependencyInjection;

namespace Megarender.MemoryStorage
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMemoryStorage(this IServiceCollection services)
        {
            services.AddTransient(typeof(ICommandStore), typeof(CommandStore));
            return services;
        }
    }    
}
