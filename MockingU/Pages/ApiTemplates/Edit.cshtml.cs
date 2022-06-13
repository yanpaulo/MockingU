using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MockingU.Data;

namespace MockingU.Pages.ApiTemplates
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly MockingU.Data.ApplicationDbContext _context;

        public EditModel(MockingU.Data.ApplicationDbContext context)
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

            var apitemplate =  await _context.ApiTemplates.FirstOrDefaultAsync(m => m.Id == id);
            if (apitemplate == null)
            {
                return NotFound();
            }
            ApiTemplate = apitemplate;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove($"{nameof(ApiTemplate)}.{nameof(ApiTemplate.UserId)}");
            ModelState.Remove($"{nameof(ApiTemplate)}.{nameof(ApiTemplate.User)}");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var entry = _context.Update(ApiTemplate);
            entry.Property(e => e.UserId).IsModified = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApiTemplateExists(ApiTemplate.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ApiTemplateExists(int id)
        {
          return (_context.ApiTemplates?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
