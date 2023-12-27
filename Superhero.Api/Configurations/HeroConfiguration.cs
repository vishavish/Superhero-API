using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Superhero.Api.Entities;

namespace Superhero.Api.Configurations
{
    public class HeroConfiguration : IEntityTypeConfiguration<Hero>
    {
        public void Configure(EntityTypeBuilder<Hero> builder)
        {
            builder.Property(h => h.HeroName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(h => h.Superpower)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(h => h.PowerLevel)
                .IsRequired();
        }
    }
}
