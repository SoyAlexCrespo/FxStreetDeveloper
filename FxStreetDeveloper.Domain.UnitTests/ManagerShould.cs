using System;
using Xunit;
using FluentAssertions;
using FxStreetDeveloper.Domain;

namespace FxStreetDeveloper.Domain.UnitTests
{
    public class ManagerShould
    {
        
        [Fact]
        public void Create()
        {
            //Arrange
            string expectedName = "Bobby Robson";
            string expectedTeamName = "England";
            int expectedYellowCards = 0;
            int expectedRedCards = 0;

            //Act
            Manager manager = new Manager(expectedName, expectedTeamName, expectedYellowCards, expectedRedCards);

            //Assert
            manager.Should().NotBeNull();
            manager.Name.Should().Be(expectedName);
            manager.TeamName.Should().Be(expectedTeamName);
            manager.YellowCards.Should().Be(expectedYellowCards);
            manager.RedCards.Should().Be(expectedRedCards);
            manager.Id.Should().NotBe(Guid.Empty);
        }
    }
}
