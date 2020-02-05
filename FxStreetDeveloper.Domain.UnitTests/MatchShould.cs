using System;
using Xunit;
using FluentAssertions;
using FxStreetDeveloper.Domain;
using System.Collections.Generic;
using System.Linq;

namespace FxStreetDeveloper.Domain.UnitTests
{
    public class MatchShould
    {

        [Fact]
        public void Create()
        {
            //Arrange
            string expectedName = "Argentina Vs England";
            IEnumerable<MatchsPlayersHouse> expectedPlayersHouse = GetPlayersHouse("Argentina");
            IEnumerable<MatchsPlayersAway> expectedPlayersAway = GetPlayersAway("Italy");
            Manager expectedManagerHouse = GetManager("Argentina");
            Manager expectedManagerAway = GetManager("Italy");

            string expectedRefereeName = "Pierluigi Collina";
            int expectedRefereeMinutesPlayed = 0;
            Referee expectedReferee = new Referee(expectedRefereeName, expectedRefereeMinutesPlayed);

            DateTime expectedDate = new DateTime(2020, 1, 13);

            //Act
            Match match = new Match(expectedName, expectedPlayersHouse.ToArray(), expectedPlayersAway.ToArray(), expectedManagerHouse, expectedManagerAway, expectedReferee, expectedDate);

            //Assert

            match.Should().NotBeNull();
            match.Name.Should().Be(expectedName);
            match.HouseTeamPlayers.Should().NotBeNull();
            match.AwayTeamPlayers.Should().NotBeNull();
            match.HouseTeamManager.Should().NotBeNull();
            match.AwayTeamManager.Should().NotBeNull();
            match.Id.Should().NotBe(Guid.Empty);
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

        private static Manager GetManager(string team)
        {
            string managerName = "Manager of " + team;
            string managerTeamName = team;
            int managerYellowCards = 0;
            int managerRedCards = 0;
            Manager manager = new Manager(managerName, managerTeamName, managerYellowCards, managerRedCards);
            return manager;
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
    }
}
