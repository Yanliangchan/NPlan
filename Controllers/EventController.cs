using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPlan.Data;
using NPlan.Models;
using System.Linq;
using System.Threading.Tasks;

namespace NPlan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Event
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return await _context.Events
                .Include(e => e.InterestGroup)
                .Include(e => e.Attendances)
                .ToListAsync();
        }

        // GET: api/Event/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var evnt = await _context.Events
                .Include(e => e.InterestGroup)
                .Include(e => e.Attendances)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (evnt == null)
            {
                return NotFound();
            }

            return evnt;
        }

        // POST: api/Event
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event evnt)
        {
            _context.Events.Add(evnt);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = evnt.Id }, evnt);
        }

        // PUT: api/Event/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, Event evnt)
        {
            if (id != evnt.Id)
            {
                return BadRequest();
            }

            _context.Entry(evnt).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Event/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var evnt = await _context.Events.FindAsync(id);
            if (evnt == null)
            {
                return NotFound();
            }

            _context.Events.Remove(evnt);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
