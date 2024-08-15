using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NPlan.Data;
using NPlan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPlan.Pages.Events
{
    public class PastEventsModel : PageModel
    {
        private readonly ApplicationDbContext _dbcontext;

        public PastEventsModel(ApplicationDbContext context)
        {
            _dbcontext = context;
        }

        public IList<Event> PastEventList { get; set; } = new List<Event>();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            DateTime today = DateTime.Now;
            IQueryable<Event> eventsQuery = _dbcontext.Events
                .Include(e => e.InterestGroup)
                .Where(e => e.EndDateTime < today)  // Corrected condition here
                .OrderByDescending(e => e.EndDateTime);

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                eventsQuery = eventsQuery.Where(e => e.EventName.Contains(SearchTerm)
                                                  || e.Description.Contains(SearchTerm)
                                                  || e.InterestGroup.Name.Contains(SearchTerm));
            }

            PastEventList = await eventsQuery.ToListAsync();
        }
    }
}
