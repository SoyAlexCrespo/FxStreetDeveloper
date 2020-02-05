using System;
using Xunit;
using FluentAssertions;
using FxStreetDeveloper.Domain;

namespace FxStreetDeveloper.Domain.UnitTests
{
    public class RefereeShould
    {
        
        [Fact]
        public void Create()
        {
            //Arrange
            string expectedName = "Pierluigi Collina";
            int expectedMinutesPlayed = 0;

            //Act
            Referee referee = new Referee(expectedName, expectedMinutesPlayed);

            //Assert
            referee.Should().NotBeNull();
            referee.Name.Should().Be(expectedName);
            referee.Id.Should().NotBe(Guid.Empty);
        }
    }
}
