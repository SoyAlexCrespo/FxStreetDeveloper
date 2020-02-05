using System;
using System.Collections.Generic;

namespace FxStreetDeveloper.Domain
{
	public class Match
	{
		public Guid Id { get; private set; }
		public string Name { get; private set; }
		public ICollection<MatchsPlayersHouse> HouseTeamPlayers { get; private set; }
		public ICollection<MatchsPlayersAway> AwayTeamPlayers { get; private set; }
		public Manager HouseTeamManager { get; private set; }
		public Manager AwayTeamManager { get; private set; }
		public Referee Referee { get; private set; }
		public DateTime Date { get; private set; }

		public Match(string name, MatchsPlayersHouse[] houseTeamPlayers, MatchsPlayersAway[] awayTeamPlayers, Manager houseTeamManager, Manager awayTeamManager, Referee referee, DateTime date)
		{
			Id = Guid.NewGuid();
			Name = name;
			HouseTeamPlayers = houseTeamPlayers;
			AwayTeamPlayers = awayTeamPlayers;
			HouseTeamManager = houseTeamManager;
			AwayTeamManager = awayTeamManager;
			Referee = referee;
			Date = date;

		}

		private Match()
		{
		}

	}
}