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
    public class MatchControllerShould
    {
        private readonly FxStreetDeveloperContext _context;

        public MatchControllerShould()
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
        public void Get_All_Matchs()
        {
            //Arrange
            MatchController controller = new MatchController(_context);

            //Act
            var result = controller.Get();

            //Assert
            var typedResult = (IEnumerable<V1.MatchResponse>)Assert.IsType<OkObjectResult>(result).Value;
            typedResult.Should().HaveCount(1);
            typedResult.Should().Contain(s => s.Name == "Argentina Vs England");
        }


        [Fact]
        public void Get_One_Match()
        {
            //Arrange
            MatchController controller = new MatchController(_context);

            IActionResult result = controller.Get();
            IEnumerable<V1.MatchResponse> typedResult = (IEnumerable<V1.MatchResponse>)Assert.IsType<OkObjectResult>(result).Value;
            int id = typedResult.ToList().First().Id;

            //Act
            var resultGet = controller.Get(id);

            //Assert
            var typedResultGet = (V1.MatchResponse)Assert.IsType<OkObjectResult>(resultGet).Value;
            typedResultGet.Should().Match<V1.MatchResponse>(m => m.Name == "Argentina Vs England");
        }

        private void Seed(FxStreetDeveloperContext context)
        {
            string homeTeamName = "Argentina";
            string awayTeamName = "England";
            string name = homeTeamName + " Vs " + awayTeamName;
            IEnumerable<MatchsPlayersHouse> playersHouse = GetPlayersHouse(homeTeamName);
            IEnumerable<MatchsPlayersAway> playersAway = GetPlayersAway(awayTeamName);
            Manager managerHouse = GetManager(homeTeamName);
            Manager managerAway = GetManager(awayTeamName);

            string refereeName = "Pierluigi Collina";
            int refereeMinutesPlayed = 100;
            Referee referee = new Referee(refereeName, refereeMinutesPlayed);

            DateTime date = new DateTime(2020, 1, 13);

            Match match = new Match(name, playersHouse.ToArray(), playersAway.ToArray(), managerHouse, managerAway, referee, date);

            context.Matchs.Add(match);
            context.SaveChanges();
        }



        private static Manager GetManager(string team)
        {
            string managerName = "Manager of " + team;
            string managerTeamName = team;
            int managerYellowCards = 0;
            int managerRedCards = 0;
            Manager manager = new Manager(managerName, managerTeamName, managerYellowCards, managerRedCards);
            return manager;
        }


        private static V1.ManagerRequest GetManagerRequest(string team)
        {
            return new V1.ManagerRequest()
            {
                Name = "Manager of " + team,
                TeamName = team,
                YellowCards = 0,
                RedCards = 0
            };
        }

        private static IEnumerable<MatchsPlayersHouse> GetPlayersHouse(string team)
        {
            for (int x = 0; x < 11; x++)
            {
                Player player = GetPlayer(team, x);

                yield return new MatchsPlayersHouse() { Player = player, PlayerId = player.Id };
            }
        }
        private static IEnumerable<MatchsPlayersAway> GetPlayersAway(string team)
        {
            for (int x = 0; x < 11; x++)
            {
                Player player = GetPlayer(team, x);

                yield return new MatchsPlayersAway() { Player = player, PlayerId = player.Id };
            }
        }


        private static Player GetPlayer(string team, int x)
        {
            string playerName = "Jugador " + team + " " + x;
            int playerNumber = x;
            string playerTeamName = team;
            int playerYellowCards = 0;
            int playerRedCards = 0;
            int playerMinutesPlayed = 90;
            return new Player(playerName, playerNumber, playerTeamName, playerYellowCards, playerRedCards, playerMinutesPlayed);
        }
    }
}
