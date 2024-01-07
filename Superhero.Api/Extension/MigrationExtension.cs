using Superhero.Api.Context;

namespace Superhero.Api.Extension
{
    public static class MigrationExtension
    {
        public static async Task InitializeDb(this WebApplication app) 
        {
            using IServiceScope scope = app.Services.CreateScope();

            var initializer = scope.ServiceProvider.GetRequiredService<AppDbInitializer>();

            await initializer.InitiailzeAsync();
        }
    }
}
