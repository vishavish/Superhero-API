using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Superhero.Api.Controllers;
using Superhero.Api.Entities;
using Superhero.Api.Interfaces;
using Superhero.Api.Models;

namespace Superhero.Api.Tests.ControllerTests
{
    public class HeroControllerTests
    {
        private readonly IHeroRepository _heroRepository;

        public HeroControllerTests()
        {
            _heroRepository = A.Fake<IHeroRepository>();
        }

        [Fact]
        public async void SuperheroController_GetSuperheroes_ReturnOK()
        {
            //Arrange
            var heroes = A.Fake<Result<List<Hero>>>();
            var heroList = A.Fake<Result<List<Hero>>>();
            var controller = new SuperheroController(_heroRepository);

            //Act
            var result = await controller.GetSuperheroes(string.Empty);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            result.Should().NotBeNull();
        }
    }
}
