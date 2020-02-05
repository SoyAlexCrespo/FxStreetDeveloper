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
        public static V1.ManagerResponse ToDto(this Manager manager)
        {
            return new V1.ManagerResponse()
            {
                Id = manager.Id.GetHashCode(),
                Name = manager.Name,
                TeamName = manager.TeamName,
                YellowCards = manager.YellowCards,
                RedCards = manager.RedCards,
            };
        }
        public static V1.RefereeResponse ToDto(this Referee referee)
        {
            return new V1.RefereeResponse()
            {
                Id = referee.Id.GetHashCode(),
                Name = referee.Name,
                MinutesPlayed = referee.MinutesPlayed,
            };
        }

        public static V1.MatchResponse ToDto(this Match match)
        {
            return new V1.MatchResponse()
            {
                Id = match.Id.GetHashCode(),
                Name = match.Name,
                HouseTeamPlayers = match.HouseTeamPlayers.Select(p => p.Player.ToDto()).ToArray(),
                AwayTeamPlayers = match.AwayTeamPlayers.Select(p => p.Player.ToDto()).ToArray(),
                HouseTeamManager = match.HouseTeamManager.ToDto(),
                AwayTeamManager = match.AwayTeamManager.ToDto(),
                Referee = match.Referee.ToDto(),
                Date = match.Date,
            };
        }


        public static V1.CardResponse ToYellowCardDto(this Player player)
        {
            return new V1.CardResponse()
            {
                Id = player.Id.GetHashCode(),
                Name = player.Name,
                TeamName = player.TeamName,
                Total = player.YellowCards,
            };
        }

        public static V1.CardResponse ToRedCardDto(this Player player)
        {
            return new V1.CardResponse()
            {
                Id = player.Id.GetHashCode(),
                Name = player.Name,
                TeamName = player.TeamName,
                Total = player.RedCards,
            };
        }

        public static V1.MinuteResponse ToMinutesDto(this Player player)
        {
            return new V1.MinuteResponse()
            {
                Id = player.Id.GetHashCode(),
                Name = player.Name,
                Total = player.RedCards,
            };
        }

        public static V1.CardResponse ToYellowCardDto(this Manager manager)
        {
            return new V1.CardResponse()
            {
                Id = manager.Id.GetHashCode(),
                Name = manager.Name,
                TeamName = manager.TeamName,
                Total = manager.YellowCards,
            };
        }

        public static V1.CardResponse ToRedCardDto(this Manager manager)
        {
            return new V1.CardResponse()
            {
                Id = manager.Id.GetHashCode(),
                Name = manager.Name,
                TeamName = manager.TeamName,
                Total = manager.RedCards,
            };
        }

        public static V1.MinuteResponse ToMinutesDto(this Referee referee)
        {
            return new V1.MinuteResponse()
            {
                Id = referee.Id.GetHashCode(),
                Name = referee.Name,
                Total = referee.MinutesPlayed,
            };
        }

        public static Player ToEntity(this V1.PlayerRequest player)
        {
            return new Player(player.Name, player.Number, player.TeamName, player.YellowCards, player.RedCards, player.MinutesPlayed);
        }

        public static Manager ToEntity(this V1.ManagerRequest manager)
        {
            return new Manager(manager.Name,  manager.TeamName, manager.YellowCards, manager.RedCards);
        }

        public static Referee ToEntity(this V1.RefereeRequest referee)
        {
            return new Referee(referee.Name, referee.MinutesPlayed);
        }
    }
}
