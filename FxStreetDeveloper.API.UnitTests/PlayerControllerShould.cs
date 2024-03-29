using System;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using FxStreetDeveloper.DataAccess;
using FxStreetDeveloper.Domain;
using System.Collections.Generic;
using FxStreetDeveloper.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using FxStreetDeveloper.API.Controllers;

namespace FxStreetDeveloper.API.UnitTests
{
    public class PlayerControllerShould
    {
        private readonly FxStreetDeveloperContext _context;

        public PlayerControllerShould()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();
            var options = new DbContextOptionsBuilder<FxStreetDeveloperContext>()
                .UseInMemoryDatabase("InMemoryDB")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            _context = new FxStreetDeveloperContext(options);

            Seed(_context);

        }

        [Fact]
        public void Get_All_Players()
        {
            //Arrange
            PlayerController controller = new PlayerController(_context);

            //Act
            var result = controller.Get();

            //Assert
            var typedResult = (IEnumerable<V1.PlayerResponse>)Assert.IsType<OkObjectResult>(result).Value;
            typedResult.Should().HaveCount(2);
            typedResult.Should().Contain(s => s.Name == "Johan Cruyff").And.Contain(s => s.Name == "Diego Armando Maradona");
        }


        [Fact]
        public void Get_One_Player()
        {
            //Arrange
            PlayerController controller = new PlayerController(_context);
            IActionResult result = controller.Get();
            IEnumerable<V1.PlayerResponse> typedResult = (IEnumerable<V1.PlayerResponse>)Assert.IsType<OkObjectResult>(result).Value;
            int id = typedResult.ToList().First().Id;

            //Act
            var resultGet = controller.Get(id);

            //Assert
            var typedResultGet = (V1.PlayerResponse)Assert.IsType<OkObjectResult>(resultGet).Value;
            typedResultGet.Should().Match<V1.PlayerResponse>(m => m.Name == "Diego Armando Maradona");
        }

        [Fact]
        public void Create_New_Player()
        {
            //Arrange
            PlayerController controller = new PlayerController(_context);
            V1.PlayerRequest player = new V1.PlayerRequest()
            {
                Name = "Roberto Baggio",
                Number = 10,
                TeamName = "Italy",
                YellowCards = 3,
                RedCards = 1,
                MinutesPlayed = 270,

            };

            //Act
            var resultCreate = controller.Create(player);
            var result = controller.Get();

            //Assert
            Assert.IsType<NoContentResult>(resultCreate);
            var typedResult = (IEnumerable<V1.PlayerResponse>)Assert.IsType<OkObjectResult>(result).Value;
            typedResult.Should().HaveCount(3);
            typedResult.Should().Contain(s => s.Name == "Johan Cruyff")
                .And.Contain(s => s.Name == "Diego Armando Maradona")
                .And.Contain(s => s.Name == "Roberto Baggio");
        }

        private void Seed(FxStreetDeveloperContext context)
        {
            Player[] players = new[]
            {
                new Player("Diego Armando Maradona", 10, "Argentina", 3, 0, 90),
                new Player("Johan Cruyff", 14, "Holland", 1, 0, 180),
            };

            context.Players.AddRange(players);
            context.SaveChanges();
        }
    }
}
