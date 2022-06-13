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
    public class DetailsModel : PageModel
    {
        private readonly MockingU.Data.ApplicationDbContext _context;

        public DetailsModel(MockingU.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public ApiTemplate ApiTemplate { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ApiTemplates == null)
            {
                return NotFound();
            }

            var apitemplate = await _context.ApiTemplates.FirstOrDefaultAsync(m => m.Id == id);
            if (apitemplate == null)
            {
                return NotFound();
            }
            else 
            {
                ApiTemplate = apitemplate;
            }
            return Page();
        }
    }
}
