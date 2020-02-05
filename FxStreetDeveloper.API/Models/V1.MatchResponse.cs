using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FxStreetDeveloper.API.Models
{
    public static partial class V1
    {
        public class MatchResponse
        {		
			public int Id { get; set; }
			public string Name { get; set; }
			public PlayerResponse[] HouseTeamPlayers { get; set; }
			public PlayerResponse[] AwayTeamPlayers { get; set; }
			public ManagerResponse HouseTeamManager { get; set; }
			public ManagerResponse AwayTeamManager { get; set; }
			public RefereeResponse Referee { get; set; }
			public DateTime Date { get; set; }
	}
    }
}
