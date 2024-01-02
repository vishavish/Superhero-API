using Microsoft.EntityFrameworkCore;
using Superhero.Api.Context;
using Superhero.Api.Entities;
using Superhero.Api.Interfaces;

namespace Superhero.Api.Repositories
{
    public class HeroRepository : IHeroRepository
    {
        private readonly AppDbContext _context;

        public HeroRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Hero?> GetHeroById(Guid id)
        {
            return await _context.Heroes
                    .AsNoTracking()
                    .Include(h => h.Organization)
                    .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<List<Hero>> GetHeroes(string? searchTerm)
        {
            return await _context.Heroes
                    .AsNoTracking()
                    .Include(h => h.Organization)
                    .Where(h => h.HeroName!.Contains(searchTerm ?? ""))
                    .ToListAsync();
        }

        public async Task<bool> Save()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public void AddHero(Hero newHero)
        {
            _context.Heroes.Add(newHero);
        }

        public void RemoveHero(Hero hero)
        {
            _context.Heroes.Remove(hero);
        }

        public void UpdateHero(Hero hero)
        {
            _context.Entry(hero).State = EntityState.Modified;
            _context.Heroes.Update(hero);
        }
    }
}
