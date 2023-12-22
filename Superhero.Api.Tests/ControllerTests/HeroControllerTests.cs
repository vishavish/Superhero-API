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
            var result = await controller.GetSuperheroes();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            result.Should().NotBeNull();
        }
    }
}

public static class IResultExtensions
{
    public static T? GetOkObjectResultValue<T>(this IActionResult result)
    {
        return (T?)Type.GetType("Microsoft.AspNetCore.Http.Result.OkObjectResult, Microsoft.AspNetCore.Http.Results")?
            .GetProperty("Value",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?
            .GetValue(result);
    }

    public static int? GetOkObjectResultStatusCode(this IActionResult result)
    {
        return (int?)Type.GetType("Microsoft.AspNetCore.Http.Result.OkObjectResult, Microsoft.AspNetCore.Http.Results")?
            .GetProperty("StatusCode",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?
            .GetValue(result);
    }

    public static int? GetNotFoundResultStatusCode(this IActionResult result)
    {
        return (int?)Type.GetType("Microsoft.AspNetCore.Http.Result.NotFoundObjectResult, Microsoft.AspNetCore.Http.Results")?
            .GetProperty("StatusCode",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?
            .GetValue(result);
    }
}
