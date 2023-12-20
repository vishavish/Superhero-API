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
        public async Task<IActionResult> GetSuperheroes()
        {
            return Ok(await _heroRepository.GetHeroes());
        }

        [HttpGet("get-hero/{id}")]
        public async Task<IActionResult> GetSuperheroById(int id)
        {
            return Ok(await _heroRepository.GetHeroById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateSuperhero(Hero hero)
        {
            _heroRepository.AddHero(hero);
            return Ok(await _heroRepository.Save());
        }

        [HttpPut("update-hero/{id}")]
        public async Task<IActionResult> UpdateSuperhero(int id, Hero hero)
        {
            var heroResult = await _heroRepository.GetHeroById(id);
            if(heroResult.Data is null)
            {
                return Ok(heroResult);
            } 

            _heroRepository.UpdateHero(hero);
            bool res = await _heroRepository.Save();

            return Ok(new Result<Hero>()
            {
                Data = res ? heroResult.Data : null,
                IsSuccess = res ? true : false,
                Message = res ? "Successfully updated hero." : "Failed to update hero."
            });
        }

        [HttpDelete("remove-hero/{id}")]
        public async Task<IActionResult> DeleteSupehero(int id)
        {
            var heroResult = await _heroRepository.GetHeroById(id);
            if (heroResult.Data is null)
            {
                return Ok(heroResult);
            }

            _heroRepository.RemoveHero(heroResult.Data);
            bool res = await _heroRepository.Save();
            
            return Ok(new Result<Hero>()
            {
                Data = res ? heroResult.Data : null,
                IsSuccess = res ? true : false,
                Message = res ? "Successfully deleted hero." : "Failed to delete hero."
            });
        }
    }
}
