using Microsoft.EntityFrameworkCore;
using Superhero.Api.Context;
using Superhero.Api.Entities;
using Superhero.Api.Interfaces;
using Superhero.Api.Models;
using System.Runtime.CompilerServices;

namespace Superhero.Api.Repositories
{
    public class HeroRepository : IHeroRepository
    {
        private readonly AppDbContext _context;

        public HeroRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Hero>> GetHeroById(int id)
        {
            var hero = await _context.Heroes.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);
            if (hero is null)
            {
                return new Result<Hero>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "Invalid hero id."
                };
            }

            return new Result<Hero>()
            {
                Data = hero,
                IsSuccess = true,
                Message = hero?.ToString()
            };
        }

        public async Task<Result<List<Hero>>> GetHeroes()
        {
            return new Result<List<Hero>>()
            {
                Data = await _context.Heroes.ToListAsync(),
                IsSuccess = true,
                Message = "Successfully retrieved list."
            };
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
