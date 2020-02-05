using System;

namespace FxStreetDeveloper.Domain
{
	public class Player
	{
		public Guid Id { get; private set; }
		public string Name { get; private set; }
		public int Number { get; private set; }
		public string TeamName { get; private set; }
		public int YellowCards { get; private set; }
		public int RedCards { get; private set; }
		public int MinutesPlayed { get; private set; }

		public Player(string name, int number, string teamName, int yellowCards, int redCards, int minutesPlayed)
		{
			Id = Guid.NewGuid();
			Name = name;
			Number = number;
			TeamName = teamName;
			YellowCards = yellowCards;
			RedCards = redCards;
			MinutesPlayed = minutesPlayed;
		}

		private Player()
		{
		}

	}
}