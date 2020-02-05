
using System;
using System.ComponentModel.DataAnnotations;

namespace FxStreetDeveloper.API.Models
{
    public static partial class V1
    {
        public class MatchRequest
        {
            [Required(AllowEmptyStrings = false)]
            public string Name { get; set; }
            
            public int[] HouseTeamPlayers { get; set; }
            public int[] AwayTeamPlayers { get; set; }
            public int HouseTeamManager { get; set; }
            public int AwayTeamManager { get; set; }
            public int Referee { get; set; }
            public DateTime Date { get; set; }
        }
    }
}
