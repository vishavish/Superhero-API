using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Superhero.Api.Entities;
using Superhero.Api.Interfaces;
using Superhero.Api.Models;
using Superhero.Api.Validators;
using System.Text.Json;

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
            return Ok(new Result<List<Hero>>()
            {
                Data = await _heroRepository.GetHeroes(),
                IsSuccess = true,
                Message = "Success"
            });
        }

        [HttpGet("get-hero/{id}")]
        public async Task<IActionResult> GetSuperheroById(int id)
        {
            var hero = await _heroRepository.GetHeroById(id);
            return Ok(new Result<Hero>()
            {
                Data = hero,
                IsSuccess = true,
                Message = hero is not null ? "Success" : "Not Found"
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateSuperhero(Hero hero)
        {
            _heroRepository.AddHero(hero);
            bool res = await _heroRepository.Save();

            return Ok(new Result<Hero>()
            {
                Data = res ? hero : null,
                IsSuccess = res ? true : false,
                Message = res ? "Success" : "Failed"
            });
        }

        [HttpPut("update-hero/{id}")]
        public async Task<IActionResult> UpdateSuperhero(int id, Hero hero)
        {
            var heroResult = await _heroRepository.GetHeroById(id);
            if(heroResult is null)
            {
                return Ok(heroResult);
            } 

            _heroRepository.UpdateHero(hero);
            bool res = await _heroRepository.Save();

            return Ok(new Result<Hero>()
            {
                Data = res ? heroResult : null,
                IsSuccess = res ? true : false,
                Message = res ? "Successfully updated hero." : "Failed to update hero."
            });
        }

        [HttpDelete("remove-hero/{id}")]
        public async Task<IActionResult> DeleteSupehero(int id)
        {
            var heroResult = await _heroRepository.GetHeroById(id);
            if (heroResult is null)
            {
                return Ok(heroResult);
            }

            _heroRepository.RemoveHero(heroResult);
            bool res = await _heroRepository.Save();
            
            return Ok(new Result<Hero>()
            {
                Data = res ? heroResult : null,
                IsSuccess = res ? true : false,
                Message = res ? "Successfully deleted hero." : "Failed to delete hero."
            });
        }
    }
}
