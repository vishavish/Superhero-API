using Superhero.Api.Interfaces;
using Superhero.Api.Repositories;

namespace Superhero.Api.Extension
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IHeroRepository, HeroRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();

            return services;
        }
    }
}
