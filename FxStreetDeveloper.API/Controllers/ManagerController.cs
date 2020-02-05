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
    public class ManagerController : ControllerBase
    {
        private readonly FxStreetDeveloperContext _context;

        public ManagerController(FxStreetDeveloperContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<V1.ManagerResponse>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(_context.Managers.Select(p => p.ToDto()));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public IActionResult Create([FromBody]V1.ManagerRequest manager)
        {
            _context.Managers.Add(manager.ToEntity());
            _context.SaveChanges();

            return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            V1.ManagerResponse managerResponse = _context.Managers.ToListAsync().Result.Select(p => p.ToDto()).Where(i => i.Id == id).FirstOrDefault();

            if (managerResponse == null) return NotFound();

            return Ok(managerResponse);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Update(int id, [FromBody]V1.ManagerRequest ManagerRequest)
        {
            Manager manager = _context.Managers.ToListAsync().Result.Where(i => i.ToDto().Id == id).FirstOrDefault();

            if (manager == null) return NotFound();



            _context.SaveChanges();

            return NoContent();
        }
    }
}