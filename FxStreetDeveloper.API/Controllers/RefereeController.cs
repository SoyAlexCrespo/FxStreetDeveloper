using FxStreetDeveloper.API.Models;
using FxStreetDeveloper.DataAccess;
using FxStreetDeveloper.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FxStreetDeveloper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefereeController : ControllerBase
    {
        private readonly FxStreetDeveloperContext _context;

        public RefereeController(FxStreetDeveloperContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<V1.RefereeResponse>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(_context.Referees.Select(p => p.ToDto()));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public IActionResult Create([FromBody]V1.RefereeRequest referee)
        {
            _context.Referees.Add(referee.ToEntity());
            _context.SaveChanges();

            return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            V1.RefereeResponse refereeResponse = _context.Referees.ToListAsync().Result.Select(p => p.ToDto()).Where(i => i.Id == id).FirstOrDefault();

            if (refereeResponse == null) return NotFound();

            return Ok(refereeResponse);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Update(int id, [FromBody]V1.RefereeRequest RefereeRequest)
        {
            Referee referee = _context.Referees.ToListAsync().Result.Where(i => i.ToDto().Id == id).FirstOrDefault();

            if (referee == null) return NotFound();



            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Delete(int id)
        {
            Referee referee = _context.Referees.ToListAsync().Result.Where(i => i.ToDto().Id == id).FirstOrDefault();

            if (referee == null) return NotFound();

            _context.Referees.Remove(referee);
            _context.SaveChanges();

            return NoContent();
        }

    }
}