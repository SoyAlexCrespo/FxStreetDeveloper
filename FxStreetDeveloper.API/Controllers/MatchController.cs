using FxStreetDeveloper.API.Models;
using FxStreetDeveloper.DataAccess;
using FxStreetDeveloper.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FxStreetDeveloper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly FxStreetDeveloperContext _context;

        public MatchController(FxStreetDeveloperContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<V1.MatchResponse>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return base.Ok(LoadMatch().Select(p => p.ToDto()));
        }

        private IIncludableQueryable<Match, Referee> LoadMatch()
        {
            return _context.Matchs
                                .Include(m => m.HouseTeamPlayers)
                                    .ThenInclude(p => p.Player)
                                .Include(m => m.AwayTeamPlayers)
                                    .ThenInclude(p => p.Player)
                                .Include(m => m.HouseTeamManager)
                                .Include(m => m.AwayTeamManager)
                                .Include(m => m.Referee);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Create([FromBody]V1.MatchRequest matchRequest)
        {

            List<MatchsPlayersHouse> playersHouse = new List<MatchsPlayersHouse>();
            List<MatchsPlayersAway> playersAway = new List<MatchsPlayersAway>();
            Manager managerHouse;
            Manager managerAway;
            Referee referee;

            try
            {
                playersHouse = GeHousePlayersById(matchRequest.HouseTeamPlayers).ToList();
                playersAway = GeAwayPlayersById(matchRequest.AwayTeamPlayers).ToList();
                managerHouse = _context.Managers.ToListAsync().Result.Where(i => i.ToDto().Id == matchRequest.HouseTeamManager).FirstOrDefault() ?? throw new KeyNotFoundException();
                managerAway = _context.Managers.ToListAsync().Result.Where(i => i.ToDto().Id == matchRequest.AwayTeamManager).FirstOrDefault() ?? throw new KeyNotFoundException();
                referee = _context.Referees.ToListAsync().Result.Where(i => i.ToDto().Id == matchRequest.Referee).FirstOrDefault() ?? throw new KeyNotFoundException();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            Match match = new Match(matchRequest.Name, playersHouse.ToArray(), playersAway.ToArray(), managerHouse, managerAway, referee, matchRequest.Date);
            _context.Matchs.Add(match);
            _context.SaveChanges();

            return NoContent();
        }

        private IEnumerable<MatchsPlayersHouse> GeHousePlayersById(int[] players)
        {
            foreach (var id in players)
            {               
                Player player = _context.Players.ToListAsync().Result.Where(p => p.ToDto().Id == id).FirstOrDefault() ?? throw new KeyNotFoundException();
                yield return new MatchsPlayersHouse() { Player = player, PlayerId = player.Id };
            }            
        }

        private IEnumerable<MatchsPlayersAway> GeAwayPlayersById(int[] players)
        {
            foreach (var id in players)
            {
                Player player = _context.Players.ToListAsync().Result.Where(p => p.ToDto().Id == id).FirstOrDefault() ?? throw new KeyNotFoundException();
                yield return new MatchsPlayersAway() { Player = player, PlayerId = player.Id };
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            V1.MatchResponse matchResponse = _context.Matchs.ToListAsync().Result.Select(p => p.ToDto()).Where(i => i.Id == id).FirstOrDefault();

            if(matchResponse == null) return NotFound();

            return Ok(matchResponse);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Update(int id, [FromBody]V1.MatchRequest matchRequest)
        {
            //TODO
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Delete(int id)
        {
            Match match = LoadMatch().ToListAsync().Result.Where(i => i.ToDto().Id == id).FirstOrDefault();

            if (match == null) return NotFound();

            _context.Matchs.Remove(match);
            _context.SaveChanges();

            return NoContent();
        }
    }
}