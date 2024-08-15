using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NPlan.Data;
using NPlan.Models;

namespace NPlan.Pages.Attendance
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<NPlan.Models.Attendance> AttendanceList { get; set; } = new List<NPlan.Models.Attendance>();

        public async Task OnGetAsync()
        {
            AttendanceList = await _context.Attendances
                .Include(a => a.User)
                .Include(a => a.Event)
                .ToListAsync();
        }
    }
}
