using Superhero.Api.Entities;
using Superhero.Api.Models;

namespace Superhero.Api.Interfaces
{
    public interface IHeroRepository
    {
        Task<Result<List<Hero>>> GetHeroes();
        Task<Result<Hero>> GetHeroById(int id);
        Task<bool> Save();
        void AddHero(Hero newHero);
        void UpdateHero(Hero hero);
        void RemoveHero(Hero hero);
    }
}
