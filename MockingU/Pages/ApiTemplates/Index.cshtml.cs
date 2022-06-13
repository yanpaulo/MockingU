using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MockingU.Data;

namespace MockingU.Pages.ApiTemplates
{

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly MockingU.Data.ApplicationDbContext _context;

        public IndexModel(MockingU.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ApiTemplate> ApiTemplate { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ApiTemplates != null)
            {
                ApiTemplate = await _context.ApiTemplates.Where(e => e.User.UserName == User.Identity.Name).ToListAsync();
            }
        }
    }
}
