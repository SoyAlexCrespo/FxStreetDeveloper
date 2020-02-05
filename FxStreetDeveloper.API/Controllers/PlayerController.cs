using FxStreetDeveloper.API.Models;
using FxStreetDeveloper.DataAccess;
using FxStreetDeveloper.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FxStreetDeveloper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly FxStreetDeveloperContext _context;

        public PlayerController(FxStreetDeveloperContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<V1.PlayerResponse>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(_context.Players.Select(p => p.ToDto()));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public IActionResult Create([FromBody]V1.PlayerRequest player)
        {
            _context.Players.Add(player.ToEntity());
            _context.SaveChanges();

            return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            V1.PlayerResponse playerResponse = _context.Players.ToListAsync().Result.Select(p => p.ToDto()).Where(i => i.Id == id).FirstOrDefault();

            if(playerResponse == null) return NotFound();

            return Ok(playerResponse);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Update(int id, [FromBody]V1.PlayerRequest playerRequest)
        {
            Player player = _context.Players.ToListAsync().Result.Where(i => i.ToDto().Id == id).FirstOrDefault();

            if (player == null) return NotFound();

            _context.Entry(player).State = EntityState.Modified;
            UpdatePlayerEntry(_context.Entry(player), playerRequest);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Delete(int id)
        {
            Player player = _context.Players.ToListAsync().Result.Where(i => i.ToDto().Id == id).FirstOrDefault();

            if (player == null) return NotFound();

            _context.Players.Remove(player);
            _context.SaveChanges();

            return NoContent();
        }

        private void UpdatePlayerEntry(EntityEntry<Player> entityEntry, V1.PlayerRequest playerRequest)
        {
            entityEntry.Property(nameof(Player.Name)).CurrentValue = playerRequest.Name;
            entityEntry.Property(nameof(Player.Number)).CurrentValue = playerRequest.Number;
            entityEntry.Property(nameof(Player.TeamName)).CurrentValue = playerRequest.TeamName;
            entityEntry.Property(nameof(Player.YellowCards)).CurrentValue = playerRequest.YellowCards;
            entityEntry.Property(nameof(Player.RedCards)).CurrentValue = playerRequest.RedCards;
            entityEntry.Property(nameof(Player.MinutesPlayed)).CurrentValue = playerRequest.MinutesPlayed;
        }
    }
}