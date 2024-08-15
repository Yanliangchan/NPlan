using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NPlan.Data;
using NPlan.Models;
using NuGet.Protocol.Core.Types;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NPlan.Pages.Events
{
    public class IndexEvents : PageModel
    {
        private readonly ApplicationDbContext _dbcontext;

        public IndexEvents(ApplicationDbContext context)
        {
            _dbcontext = context;
        }

        public IList<Event> EventList { get; set; } = new List<Event>();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            IQueryable<Event> eventsQuery = _dbcontext.Events.Include(e => e.InterestGroup)
                                                           .OrderBy(e => e.StartDateTime)
                                                           .Where(e => e.EndDateTime >= DateTime.Now);

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                eventsQuery = eventsQuery.Where(e => e.EventName.Contains(SearchTerm)
                                                  || e.Description.Contains(SearchTerm)
                                                  || e.InterestGroup.Name.Contains(SearchTerm));
            }

            EventList = await eventsQuery.Take(5).ToListAsync();
        }

        public async Task<IActionResult> OnPostSignUpAsync(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var userEvent = new UserEvent
            {
                UserId = userId,
                EventId = id
            };

            _dbcontext.UserEvents.Add(userEvent);
            await _dbcontext.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
