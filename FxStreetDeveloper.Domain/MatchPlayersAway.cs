using System;
using System.Collections.Generic;

namespace FxStreetDeveloper.Domain
{
	public class MatchsPlayersAway
	{
		public Guid MatchId { get; set; }
		public Match Match { get; set; }
		public Guid PlayerId { get; set; }
		public Player Player { get; set; }

	}
}