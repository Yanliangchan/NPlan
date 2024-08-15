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
    public class Index : PageModel
    {
        private readonly ApplicationDbContext _dbcontext;

        public Index(ApplicationDbContext context)
        {
            _dbcontext = context;
        }

        public IList<Event> EventList { get; set; } = new List<Event>();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            DateTime today = DateTime.Now;
            IQueryable<Event> eventsQuery = _dbcontext.Events
                .Include(e => e.InterestGroup)
                .Where(e => e.EndDateTime >= today)
                .OrderBy(e => e.StartDateTime);

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                eventsQuery = eventsQuery.Where(e => e.EventName.Contains(SearchTerm)
                                                  || e.Description.Contains(SearchTerm)
                                                  || e.InterestGroup.Name.Contains(SearchTerm));
            }

            EventList = await eventsQuery.Take(5).ToListAsync();
        }
    }
}
