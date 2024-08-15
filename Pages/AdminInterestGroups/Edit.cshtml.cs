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

namespace NPlan.Pages.InterestGroups
{
    public class EditModel : PageModel
    {
        private readonly NPlan.Data.ApplicationDbContext _context;

        public EditModel(NPlan.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InterestGroup InterestGroup { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interestgroup =  await _context.InterestGroups.FirstOrDefaultAsync(m => m.Id == id);
            if (interestgroup == null)
            {
                return NotFound();
            }
            InterestGroup = interestgroup;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(InterestGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterestGroupExists(InterestGroup.Id))
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

        private bool InterestGroupExists(int id)
        {
            return _context.InterestGroups.Any(e => e.Id == id);
        }
    }
}
