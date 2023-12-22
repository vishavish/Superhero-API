using Microsoft.AspNetCore.Mvc;
using Superhero.Api.Entities;
using Superhero.Api.Interfaces;
using Superhero.Api.Models;

namespace Superhero.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuperheroController : ControllerBase
    {
        private IHeroRepository _heroRepository;

        public SuperheroController(IHeroRepository heroRepository)
        {
            _heroRepository = heroRepository;
        }

        [HttpGet]
        public async Task<Result<List<Hero>>> GetSuperheroes(string? searchTerm)
        {
            return Result<List<Hero>>.Success(await _heroRepository.GetHeroes(searchTerm));
        }

        [HttpGet("get-hero/{id}")]
        public async Task<Result<Hero>> GetSuperheroById(int id)
        {
            var hero = await _heroRepository.GetHeroById(id);
            if(hero is null)
            {
                return Result<Hero>.Failure("Hero not found.");
            }

            return Result<Hero>.Success(hero);
        }

        [HttpPost]
        public async Task<Result<Hero>> CreateSuperhero(Hero hero)
        {
            _heroRepository.AddHero(hero);
            await _heroRepository.Save();

            return Result<Hero>.Success(hero);
        }

        [HttpPut("update-hero/{id}")]
        public async Task<Result<string>> UpdateSuperhero(int id, Hero hero)
        {
            var heroResult = await _heroRepository.GetHeroById(id);
            if(heroResult is null)
            {
                return Result<string>.Failure("Hero not found.");
            } 

            _heroRepository.UpdateHero(hero);
            await _heroRepository.Save();

            return Result<string>.Success("Successfully updated hero.");
        }

        [HttpDelete("remove-hero/{id}")]
        public async Task<Result<string>> DeleteSupehero(int id)
        {
            var heroResult = await _heroRepository.GetHeroById(id);
            if (heroResult is null)
            {
                return Result<string>.Failure("Hero not found.");
            }

            _heroRepository.RemoveHero(heroResult);
            await _heroRepository.Save();

            return Result<string>.Success("Sucessfully deleted hero.");
        }
    }
}
