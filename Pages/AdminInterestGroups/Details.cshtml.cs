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
    public class DetailsModel : PageModel
    {
        private readonly NPlan.Data.ApplicationDbContext _context;

        public DetailsModel(NPlan.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
