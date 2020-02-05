using FxStreetDeveloper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FxStreetDeveloper.API.Models
{
    public static class Extensions
    {
        public static V1.PlayerResponse ToDto(this Player player)
        {
            return new V1.PlayerResponse()
            {
                Id = player.Id.GetHashCode(),
                Name = player.Name,
                Number = player.Number,
                TeamName = player.TeamName,
                YellowCards = player.YellowCards,
                RedCards = player.RedCards,
                MinutesPlayed = player.MinutesPlayed,
            };
        }
    }
}
