using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MockingU.Data;

namespace MockingU.Pages.ApiTemplates
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly MockingU.Data.ApplicationDbContext _context;

        public CreateModel(MockingU.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ApiTemplate ApiTemplate { get; set; } = new ApiTemplate();


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove($"{nameof(ApiTemplate)}.{nameof(ApiTemplate.UserId)}");
            ModelState.Remove($"{nameof(ApiTemplate)}.{nameof(ApiTemplate.User)}");
            
            if (!ModelState.IsValid || _context.ApiTemplates == null || ApiTemplate == null)
            {
                return Page();
            }

            ApiTemplate.UserId = _context.Users.Single(u => u.UserName == User.Identity.Name).Id;
            _context.ApiTemplates.Add(ApiTemplate);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
