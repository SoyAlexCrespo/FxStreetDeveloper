using FxStreetDeveloper.API.Models;
using FxStreetDeveloper.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FxStreetDeveloper.API
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
        public async Task<IActionResult> Get()
        {
            return Ok(_context.Players.Select(p => p.ToDto()));
        }
    }
}