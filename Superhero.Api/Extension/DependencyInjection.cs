using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

namespace Superhero.Api.Extension
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
