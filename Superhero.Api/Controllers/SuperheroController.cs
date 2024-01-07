using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superhero.Api.Entities;
using Superhero.Api.Interfaces;
using Superhero.Api.Models;

namespace Superhero.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SuperheroController : ControllerBase
    {
        private readonly IHeroRepository _heroRepository;
        private readonly ILogger<SuperheroController> _logger;

        public SuperheroController(IHeroRepository heroRepository, ILogger<SuperheroController> logger)
        {
            _heroRepository = heroRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetSuperheroes(string? searchTerm)
        {
            return Ok(Result<List<Hero>>.Success(await _heroRepository.GetHeroes(searchTerm)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSuperheroById(Guid id)
        {
            var hero = await _heroRepository.GetHeroById(id);
            if(hero is null)
            {
                _logger.LogInformation("Hero {Id} is not found.", id);
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: $"Couldn't find an hero with id {id}");
            }

            return Ok(Result<Hero>.Success(hero));
        }

        [HttpPost]
        public async Task<IActionResult> CreateSuperhero(Hero hero)
        {
            _heroRepository.AddHero(hero);
            await _heroRepository.Save();

            return Ok(Result<Hero>.Success(hero));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSuperhero(Guid id, Hero hero)
        {
            var heroResult = await _heroRepository.GetHeroById(id);
            if(heroResult is null)
            {
                _logger.LogInformation("Hero {Id} is not found.", id);
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: $"Couldn't find an hero with id {id}");
            } 

            _heroRepository.UpdateHero(hero);
            await _heroRepository.Save();

            return Ok(Result<string>.Success("Successfully updated hero."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuperhero(Guid id)
        {
            var heroResult = await _heroRepository.GetHeroById(id);
            if (heroResult is null)
            {
                _logger.LogInformation("Hero {Id} is not found.", id);
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: $"Couldn't find an hero with id {id}");
            }

            _heroRepository.RemoveHero(heroResult);
            await _heroRepository.Save();

            return Ok(Result<string>.Success("Sucessfully deleted hero."));
        }
    }
}
