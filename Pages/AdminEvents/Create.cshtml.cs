using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NPlan.Data;
using NPlan.Models;
using System.Threading.Tasks;

namespace NPlan.Pages.Events
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Event Event { get; set; } = new Event();

        public IActionResult OnGet()
        {
            ViewData["InterestGroupId"] = new SelectList(_context.InterestGroups, "Id", "Name");
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            System.Diagnostics.Debug.WriteLine("CheckingState");
            if (!ModelState.IsValid)
            {
                // Re-populate the dropdown list if there is a validation error
                ViewData["InterestGroupId"] = new SelectList(_context.InterestGroups, "Id", "Name");
                System.Diagnostics.Debug.WriteLine(ViewData["InterestGroupId"]);
                
            }

                // Add the new event to the database
            System.Diagnostics.Debug.WriteLine("AddingEvent");
            System.Diagnostics.Debug.WriteLine(Event);
            _context.Events.Add(Event);
            await _context.SaveChangesAsync();
            System.Diagnostics.Debug.WriteLine("Event created successfully.");
            return RedirectToPage("./Index");
            
        }
    }
}
