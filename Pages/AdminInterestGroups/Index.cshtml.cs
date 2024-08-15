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
    public class IndexModel : PageModel
    {
        private readonly NPlan.Data.ApplicationDbContext _context;

        public IndexModel(NPlan.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<InterestGroup> InterestGroup { get;set; } = default!;

        public async Task OnGetAsync()
        {
            InterestGroup = await _context.InterestGroups.ToListAsync();
        }
    }
}
