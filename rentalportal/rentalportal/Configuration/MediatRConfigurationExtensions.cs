using MediatR;
using Microsoft.Extensions.DependencyInjection;
using rentalportal.domain.services;

namespace rentalportal.api.Configuration
{
    public static class MediatRConfigurationExtensions
    {
        public static void ConfigureMediatR(this IServiceCollection services)
        {
            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddMediatR(
                typeof(CommandResult<>).Assembly          // Services assembly
            );
        }
    }
}
