using Microsoft.EntityFrameworkCore;
using Superhero.Api.Context;

namespace Superhero.Api.Extension
{
    public static class MigrationExtension
    {
        public static void ApplyMigrations(this IApplicationBuilder app) 
        {
            using IServiceScope scope =
                app.ApplicationServices
                .CreateScope();

            using AppDbContext dbContext =
                scope.ServiceProvider
                .GetRequiredService<AppDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
