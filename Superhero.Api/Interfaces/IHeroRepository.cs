using Superhero.Api.Entities;

namespace Superhero.Api.Interfaces
{
    public interface IHeroRepository
    {
        Task<List<Hero>> GetHeroes(string? searchTerm);
        Task<Hero> GetHeroById(int id);
        Task<bool> Save();
        void AddHero(Hero newHero);
        void UpdateHero(Hero hero);
        void RemoveHero(Hero hero);
    }
}
