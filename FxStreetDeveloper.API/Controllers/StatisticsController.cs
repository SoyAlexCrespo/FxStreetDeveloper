using FxStreetDeveloper.API.Models;
using FxStreetDeveloper.DataAccess;
using FxStreetDeveloper.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FxStreetDeveloper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly FxStreetDeveloperContext _context;

        public StatisticsController(FxStreetDeveloperContext context)
        {
            _context = context;
        }

        [HttpGet("yellowcards")]
        [ProducesResponseType(typeof(IEnumerable<V1.CardResponse>), StatusCodes.Status200OK)]
        public IActionResult GetYellowCards()
        {
            return Ok(GetYellowCardsResponse());
        }

        private IEnumerable<V1.CardResponse> GetYellowCardsResponse()
        {
            foreach (Player player in _context.Players.ToListAsync().Result) yield return player.ToYellowCardDto();
            foreach (Manager manager in _context.Managers.ToListAsync().Result) yield return manager.ToYellowCardDto();
        }

        [HttpGet("redcards")]
        [ProducesResponseType(typeof(IEnumerable<V1.CardResponse>), StatusCodes.Status200OK)]
        public IActionResult GetRedCards()
        {
            return Ok(GetRedCardsResponse());
        }

        private IEnumerable<V1.CardResponse> GetRedCardsResponse()
        {
            foreach (Player player in _context.Players.ToListAsync().Result) yield return player.ToRedCardDto();
            foreach (Manager manager in _context.Managers.ToListAsync().Result) yield return manager.ToRedCardDto();
        }

        [HttpGet("minutes")]
        [ProducesResponseType(typeof(IEnumerable<V1.MinuteResponse>), StatusCodes.Status200OK)]
        public IActionResult GetMinutes()
        {
            return Ok(GetMinutesResponse());
        }

        private IEnumerable<V1.MinuteResponse> GetMinutesResponse()
        {
            foreach (var player in _context.Players.ToListAsync().Result) yield return player.ToMinutesDto();
            foreach (var referee in _context.Referees.ToListAsync().Result) yield return referee.ToMinutesDto();
        }
    }
}