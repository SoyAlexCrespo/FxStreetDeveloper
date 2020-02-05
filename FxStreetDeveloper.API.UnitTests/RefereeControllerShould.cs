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
    public class RefereeControllerShould
    {
        private readonly FxStreetDeveloperContext _context;

        public RefereeControllerShould()
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
        public void Get_All_Referees()
        {
            //Arrange
            RefereeController controller = new RefereeController(_context);

            //Act
            var result = controller.Get();

            //Assert
            var typedResult = (IEnumerable<V1.RefereeResponse>)Assert.IsType<OkObjectResult>(result).Value;
            typedResult.Should().HaveCount(2);
            typedResult.Should().Contain(s => s.Name == "Eduardo Iturralde").And.Contain(s => s.Name == "Graham Poll");
        }


        [Fact]
        public void Get_One_Referee()
        {
            //Arrange
            RefereeController controller = new RefereeController(_context);
            IActionResult result = controller.Get();
            IEnumerable<V1.RefereeResponse> typedResult = (IEnumerable<V1.RefereeResponse>)Assert.IsType<OkObjectResult>(result).Value;
            int id = typedResult.ToList().First().Id;

            //Act
            var resultGet = controller.Get(id);

            //Assert
            var typedResultGet = (V1.RefereeResponse)Assert.IsType<OkObjectResult>(resultGet).Value;
            typedResultGet.Should().Match<V1.RefereeResponse>(m => m.Name == "Eduardo Iturralde");
        }

        [Fact]
        public void Create_New_Referee()
        {
            //Arrange
            RefereeController controller = new RefereeController(_context);
            V1.RefereeRequest referee = new V1.RefereeRequest()
            {
                Name = "Pierluigi Collina",
                MinutesPlayed = 270,

            };

            //Act
            var resultCreate = controller.Create(referee);
            var result = controller.Get();

            //Assert
            Assert.IsType<NoContentResult>(resultCreate);
            var typedResult = (IEnumerable<V1.RefereeResponse>)Assert.IsType<OkObjectResult>(result).Value;
            typedResult.Should().HaveCount(3);
            typedResult.Should().Contain(s => s.Name == "Pierluigi Collina")
                .And.Contain(s => s.Name == "Eduardo Iturralde")
                .And.Contain(s => s.Name == "Graham Poll");
        }

        private void Seed(FxStreetDeveloperContext context)
        {
            Referee[] referees = new[]
            {
                new Referee("Eduardo Iturralde", 90),
                new Referee("Graham Poll", 180),
            };

            context.Referees.AddRange(referees);
            context.SaveChanges();
        }
    }
}
