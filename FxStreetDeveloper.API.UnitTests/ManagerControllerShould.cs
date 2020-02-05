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
    public class ManagerControllerShould
    {
        private readonly FxStreetDeveloperContext _context;

        public ManagerControllerShould()
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
        public void Get_All_Managers()
        {
            //Arrange
            ManagerController controller = new ManagerController(_context);

            //Act
            var result = controller.Get();

            //Assert
            var typedResult = (IEnumerable<V1.ManagerResponse>)Assert.IsType<OkObjectResult>(result).Value;
            typedResult.Should().HaveCount(2);
            typedResult.Should().Contain(s => s.Name == "Fabio Capello").And.Contain(s => s.Name == "Luis Aragonés");
        }


        [Fact]
        public void Get_One_Manager()
        {
            //Arrange
            ManagerController controller = new ManagerController(_context);
            IActionResult result = controller.Get();
            IEnumerable<V1.ManagerResponse> typedResult = (IEnumerable<V1.ManagerResponse>)Assert.IsType<OkObjectResult>(result).Value;
            int id = typedResult.ToList().First().Id;

            //Act
            var resultGet = controller.Get(id);

            //Assert
            var typedResultGet = (V1.ManagerResponse)Assert.IsType<OkObjectResult>(resultGet).Value;
            typedResultGet.Should().Match<V1.ManagerResponse>(m => m.Name == "Fabio Capello");
        }

        [Fact]
        public void Create_New_Manager()
        {
            //Arrange
            ManagerController controller = new ManagerController(_context);
            V1.ManagerRequest manager = new V1.ManagerRequest()
            {
                Name = "Bobby Robson",
                TeamName = "England",
                YellowCards = 3,
                RedCards = 1,

            };

            //Act
            var resultCreate = controller.Create(manager);
            var result = controller.Get();

            //Assert
            Assert.IsType<NoContentResult>(resultCreate);
            var typedResult = (IEnumerable<V1.ManagerResponse>)Assert.IsType<OkObjectResult>(result).Value;
            typedResult.Should().HaveCount(3);
            typedResult.Should().Contain(s => s.Name == "Bobby Robson")
                .And.Contain(s => s.Name == "Fabio Capello")
                .And.Contain(s => s.Name == "Luis Aragonés");
        }

        private void Seed(FxStreetDeveloperContext context)
        {
            Manager[] managers = new[]
            {
                new Manager("Fabio Capello", "Italy", 3, 0),
                new Manager("Luis Aragonés", "Spain", 1, 0),
            };

            context.Managers.AddRange(managers);
            context.SaveChanges();
        }
    }
}
