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
    public class InterestGroupController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InterestGroupController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/InterestGroup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InterestGroup>>> GetInterestGroups()
        {
            return await _context.InterestGroups
                .Include(ig => ig.Events)
                .Include(ig => ig.Users)
                .ToListAsync();
        }

        // GET: api/InterestGroup/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InterestGroup>> GetInterestGroup(int id)
        {
            var interestGroup = await _context.InterestGroups
                .Include(ig => ig.Events)
                .Include(ig => ig.Users)
                .FirstOrDefaultAsync(ig => ig.Id == id);

            if (interestGroup == null)
            {
                return NotFound();
            }

            return interestGroup;
        }

        // POST: api/InterestGroup
        [HttpPost]
        public async Task<ActionResult<InterestGroup>> PostInterestGroup(InterestGroup interestGroup)
        {
            _context.InterestGroups.Add(interestGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInterestGroup", new { id = interestGroup.Id }, interestGroup);
        }

        // PUT: api/InterestGroup/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInterestGroup(int id, InterestGroup interestGroup)
        {
            if (id != interestGroup.Id)
            {
                return BadRequest();
            }

            _context.Entry(interestGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterestGroupExists(id))
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

        // DELETE: api/InterestGroup/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInterestGroup(int id)
        {
            var interestGroup = await _context.InterestGroups.FindAsync(id);
            if (interestGroup == null)
            {
                return NotFound();
            }

            _context.InterestGroups.Remove(interestGroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InterestGroupExists(int id)
        {
            return _context.InterestGroups.Any(e => e.Id == id);
        }
    }
}
