using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Superhero.Api.Entities;
using Superhero.Api.Entities.Auth;
using System.Reflection;

namespace Superhero.Api.Context;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options) 
    { }

    public DbSet<Hero>? Heroes { get; set; } = null;
    public DbSet<Organization>? Organizations { get; set; } = null;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
