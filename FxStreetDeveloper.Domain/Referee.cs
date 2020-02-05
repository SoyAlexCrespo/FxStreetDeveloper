using System;

namespace FxStreetDeveloper.Domain
{
	public class Referee
	{
		public Guid Id { get; private set; }
		public string Name { get; private set; }
		public int MinutesPlayed { get; private set; }

		public Referee(string name, int minutesPlayed)
		{
			Id = Guid.NewGuid();
			Name = name;
			MinutesPlayed = minutesPlayed;
		}

		private Referee()
		{
		}

	}
}