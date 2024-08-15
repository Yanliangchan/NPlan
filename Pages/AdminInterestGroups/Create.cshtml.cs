using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NPlan.Data;
using NPlan.Models;

namespace NPlan.Pages.InterestGroups
{
    public class CreateModel : PageModel
    {
        private readonly NPlan.Data.ApplicationDbContext _context;

        public CreateModel(NPlan.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public InterestGroup InterestGroup { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.InterestGroups.Add(InterestGroup);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
