using System;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using FxStreetDeveloper.DataAccess;
using FxStreetDeveloper.Domain;
using System.Collections.Generic;
using FxStreetDeveloper.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace FxStreetDeveloper.API.UnitTests
{
    public class PlayerControllerShould
    {
        [Fact]
        public async void Get_All_Players()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<FxStreetDeveloperContext>()
                .UseInMemoryDatabase("InMemoryDB")
                .Options;

            var context = new FxStreetDeveloperContext(options);
            Seed(context);

            PlayerController controller = new PlayerController(context);

            //Act
            var result = await controller.Get();

            //Assert
            var typedResult = (IEnumerable<V1.PlayerResponse>)Assert.IsType<OkObjectResult>(result).Value;
            typedResult.Should().HaveCount(2);
            typedResult.Should().Contain(s => s.Name == "Johan Cruyff").And.Contain(s => s.Name == "Diego Armando Maradona");
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
