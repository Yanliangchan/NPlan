using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NPlan.Data;
using NPlan.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPlan.Pages.InterestGroups
{
    public class Index : PageModel
    {
        private readonly ApplicationDbContext _dbcontext;

        public Index(ApplicationDbContext context)
        {
            _dbcontext = context;
        }

        public IList<InterestGroup> InterestGroup { get; set; } = new List<InterestGroup>();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            IQueryable<InterestGroup> interestGroupsQuery = _dbcontext.InterestGroups;

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                interestGroupsQuery = interestGroupsQuery.Where(ig => ig.Name.Contains(SearchTerm)
                                                                   || ig.Description.Contains(SearchTerm)
                                                                   || ig.InterestGroupType.Contains(SearchTerm));
            }

            InterestGroup = await interestGroupsQuery.ToListAsync();
        }
    }
}
