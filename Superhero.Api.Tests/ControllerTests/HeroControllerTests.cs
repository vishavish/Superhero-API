using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Superhero.Api.Controllers;
using Superhero.Api.Entities;
using Superhero.Api.Interfaces;
using Superhero.Api.Models;

namespace Superhero.Api.Tests.ControllerTests
{
    public class HeroControllerTests
    {
        private readonly IHeroRepository _heroRepository;
        private readonly ILogger<SuperheroController> _logger;

        public HeroControllerTests()
        {
            _heroRepository = A.Fake<IHeroRepository>();
            _logger = A.Fake<ILogger<SuperheroController>>();
        }

        [Fact]
        public async void SuperheroController_GetSuperheroes_ReturnOK()
        {
            //Arrange
            var controller = new SuperheroController(_heroRepository, _logger);

            //Act
            var result = await controller.GetSuperheroes(string.Empty);

            //Assert
            result.Should().BeOfType<Result<List<Hero>>>();
            result.Should().NotBeNull();
        }
    }
}
