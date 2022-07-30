using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.JobWorkCategory
{
    public class EditModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public EditModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public JobWorkcategories JobWorkcategories { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TblJobWorkCategory == null)
            {
                return NotFound();
            }

            var jobworkcategories =  await _context.TblJobWorkCategory.FirstOrDefaultAsync(m => m.Id == id);
            if (jobworkcategories == null)
            {
                return NotFound();
            }
            JobWorkcategories = jobworkcategories;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(JobWorkcategories).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobWorkcategoriesExists(JobWorkcategories.Id))
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

        private bool JobWorkcategoriesExists(int id)
        {
          return (_context.TblJobWorkCategory?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
