using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NPlan.Data;
using NPlan.Models;

namespace NPlan.Pages.InterestGroups
{
    public class DeleteModel : PageModel
    {
        private readonly NPlan.Data.ApplicationDbContext _context;

        public DeleteModel(NPlan.Data.ApplicationDbContext context)
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

            var interestgroup = await _context.InterestGroups.FirstOrDefaultAsync(m => m.Id == id);

            if (interestgroup == null)
            {
                return NotFound();
            }
            else
            {
                InterestGroup = interestgroup;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interestgroup = await _context.InterestGroups.FindAsync(id);
            if (interestgroup != null)
            {
                InterestGroup = interestgroup;
                _context.InterestGroups.Remove(InterestGroup);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
