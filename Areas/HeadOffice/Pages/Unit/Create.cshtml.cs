using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.Unit
{
    public class CreateModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public CreateModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
            {
                return Page();
            }
            else
            {
                return Redirect("~/Index");
            }
        }

        [BindProperty]
        public Units Units { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.TblUnits == null || Units == null)
            {
                return Page();
            }

            _context.TblUnits.Add(Units);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
