using System;
using Xunit;
using FluentAssertions;
using FxStreetDeveloper.Domain;

namespace FxStreetDeveloper.Domain.UnitTests
{
    public class PlayerShould
    {
        
        [Fact]
        public void Create()
        {
            //Arrange
            string expectedName = "Diego Armando Maradona";
            int expectedNumber = 10;
            string expectedTeamName = "Argentina";
            int expectedYellowCards = 3;
            int expectedRedCards = 0;
            int expectedMinutesPlayed = 90;

            //Act
            Player player = new Player(expectedName, expectedNumber, expectedTeamName, expectedYellowCards, expectedRedCards, expectedMinutesPlayed);

            //Assert
            player.Should().NotBeNull();
            player.Name.Should().Be(expectedName);
            player.Number.Should().Be(expectedNumber);
            player.TeamName.Should().Be(expectedTeamName);
            player.YellowCards.Should().Be(expectedYellowCards);
            player.RedCards.Should().Be(expectedRedCards);
            player.MinutesPlayed.Should().Be(expectedMinutesPlayed);
            player.Id.Should().NotBe(Guid.Empty);
        }
    }
}
