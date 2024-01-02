﻿using Microsoft.EntityFrameworkCore;
using Superhero.Api.Entities;
using System.Reflection;

namespace Superhero.Api.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) 
        { }

        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Organization> Organizations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
