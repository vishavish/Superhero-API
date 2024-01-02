using FakeItEasy;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Superhero.Api.Controllers;
using Superhero.Api.Entities;
using Superhero.Api.Interfaces;

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
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void SuperheroController_GetSuperheroById_ShouldReturn404()
        {
            //Arrange
            var guid = Guid.NewGuid();
            A.CallTo(() => _heroRepository.GetHeroById(guid)).Returns(null as Hero);

            var controller = new SuperheroController(_heroRepository, _logger);

            //Act
            var result = await controller.GetSuperheroById(guid) as ObjectResult;

            //Assert
            var problemDetails = result!.Value.Should().BeAssignableTo<ProblemDetails>().Subject;
            problemDetails.Detail.Should().Contain("Couldn't find an hero with id");

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
        }
    }
}
