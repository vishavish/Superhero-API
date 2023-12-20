using Microsoft.EntityFrameworkCore;
using Superhero.Api.Entities;

namespace Superhero.Api.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Hero> Heroes { get; set; }
    }
}
