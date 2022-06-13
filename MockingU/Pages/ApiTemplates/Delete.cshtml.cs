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
    public class DeleteModel : PageModel
    {
        private readonly MockingU.Data.ApplicationDbContext _context;

        public DeleteModel(MockingU.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.ApiTemplates == null)
            {
                return NotFound();
            }
            var apitemplate = await _context.ApiTemplates.FindAsync(id);

            if (apitemplate != null)
            {
                ApiTemplate = apitemplate;
                _context.ApiTemplates.Remove(ApiTemplate);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
