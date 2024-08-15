using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NPlan.Data;
using NPlan.Models;

namespace NPlan.Pages.Events
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Event Event { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventEntity = await _context.Events
                                            .Include(e => e.InterestGroup) // Include related InterestGroup if needed
                                            .FirstOrDefaultAsync(m => m.Id == id);
            if (eventEntity == null)
            {
                return NotFound();
            }

            Event = eventEntity;
            ViewData["InterestGroupId"] = new SelectList(_context.InterestGroups, "Id", "Name", Event.InterestGroupId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["InterestGroupId"] = new SelectList(_context.InterestGroups, "Id", "Name", Event.InterestGroupId);
                
            }

            var eventToUpdate = await _context.Events.FindAsync(Event.Id);
            if (eventToUpdate == null)
            {
                return NotFound();
            }

            eventToUpdate.EventName = Event.EventName;
            eventToUpdate.Description = Event.Description;
            eventToUpdate.StartDateTime = Event.StartDateTime;
            eventToUpdate.EndDateTime = Event.EndDateTime;
            eventToUpdate.Location = Event.Location;
            eventToUpdate.Points = Event.Points;
            eventToUpdate.InterestGroupId = Event.InterestGroupId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(Event.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
