using FxStreetDeveloper.API.Models;
using FxStreetDeveloper.DataAccess;
using FxStreetDeveloper.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace FxStreetDeveloper.API.Services
{
    public class MatchTimeWatchService
    {
        IServiceProvider _serviceProvider;

        public MatchTimeWatchService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void SendInminentMatchs(int minutes)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            using (FxStreetDeveloperContext context = scope.ServiceProvider.GetRequiredService<FxStreetDeveloperContext>())
            {
                List<Match> inminentMatchs = LoadMatch(context).ToListAsync().Result.Where(m => IsInminentMatch(m.Date, minutes)).ToList();
                if (inminentMatchs.Count > 0)
                {
                    List<int> incorrectAligments = GetIncorrectAligments(inminentMatchs).ToList();

                    WebClient webClient = new WebClient();
                    webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    string address = "http://interview-api.azurewebsites.net/api/IncorrectAlignment";
                    string jsonIncorrectAligments = JsonConvert.SerializeObject(incorrectAligments);
                    webClient.UploadString(address, jsonIncorrectAligments);
                }
            }
        }

        private IIncludableQueryable<Match, Referee> LoadMatch(FxStreetDeveloperContext context)
        {
            return context.Matchs
                                .Include(m => m.HouseTeamPlayers)
                                    .ThenInclude(p => p.Player)
                                .Include(m => m.AwayTeamPlayers)
                                    .ThenInclude(p => p.Player)
                                .Include(m => m.HouseTeamManager)
                                .Include(m => m.AwayTeamManager)
                                .Include(m => m.Referee);
        }

        private IEnumerable<int> GetIncorrectAligments(List<Match> inminentMatchs)
        {
            foreach (int id in inminentMatchs.SelectMany(m => m.HouseTeamPlayers.Where(p => IsIncorrectParticipant(p.Player.YellowCards, p.Player.RedCards))).Select(p => p.Player.ToDto().Id))
            {
                yield return id;
            }
            foreach (int id in inminentMatchs.SelectMany(m => m.AwayTeamPlayers.Where(p => IsIncorrectParticipant(p.Player.YellowCards, p.Player.RedCards))).Select(p => p.Player.ToDto().Id))
            {
                yield return id;
            }
            foreach (int id in inminentMatchs.Where(p => IsIncorrectParticipant(p.HouseTeamManager.YellowCards, p.HouseTeamManager.RedCards)).Select(p => p.HouseTeamManager.ToDto().Id))
            {
                yield return id;
            }
            foreach (int id in inminentMatchs.Where(p => IsIncorrectParticipant(p.AwayTeamManager.YellowCards, p.AwayTeamManager.RedCards)).Select(p => p.AwayTeamManager.ToDto().Id))
            {
                yield return id;
            }
        }

        private bool IsIncorrectParticipant(int yellowCards, int redCards)
        {
            return yellowCards == 5 || redCards == 1;
        }

        private bool IsInminentMatch(DateTime date, int minutes)
        {
            DateTime dateNow = DateTime.UtcNow;

            return dateNow.AddMinutes(minutes) >= date && dateNow < date;
        }
    }
}