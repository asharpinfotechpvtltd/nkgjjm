using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.JobWorkCategory
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
            return Page();
        }

        [BindProperty]
        public JobWorkcategories JobWorkcategories { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.TblJobWorkCategory == null || JobWorkcategories == null)
            {
                return Page();
            }

            _context.TblJobWorkCategory.Add(JobWorkcategories);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
