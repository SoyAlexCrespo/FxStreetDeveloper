﻿
using System.ComponentModel.DataAnnotations;

namespace FxStreetDeveloper.API.Models
{
    public static partial class V1
    {
        public class PlayerRequest
        {
            [Required(AllowEmptyStrings = false)]
            public string Name { get; set; }

            public int Number { get; set; }

            [Required(AllowEmptyStrings = false)]
            public string TeamName { get; set; }

            public int YellowCards { get; set; }

            public int RedCards { get; set; }

            public int MinutesPlayed { get; set; }
        }
    }
}
