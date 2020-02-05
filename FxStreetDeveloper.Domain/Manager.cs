using System;

namespace FxStreetDeveloper.Domain
{
	public class Manager
	{
		public Guid Id { get; private set; }
		public string Name { get; private set; }
		public string TeamName { get; private set; }
		public int YellowCards { get; private set; }
		public int RedCards { get; private set; }

		public Manager(string name,  string teamName, int yellowCards, int redCards)
		{
			Id = Guid.NewGuid();
			Name = name;
			TeamName = teamName;
			YellowCards = yellowCards;
			RedCards = redCards;
		}

		private Manager()
		{
		}

	}
}