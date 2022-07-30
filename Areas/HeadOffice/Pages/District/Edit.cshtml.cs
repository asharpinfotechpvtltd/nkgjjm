using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.District
{
    public class EditModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public EditModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Districts Districts { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TblDistrict == null)
            {
                return NotFound();
            }

            var districts =  await _context.TblDistrict.FirstOrDefaultAsync(m => m.id == id);
            if (districts == null)
            {
                return NotFound();
            }
            Districts = districts;
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

            _context.Attach(Districts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DistrictsExists(Districts.id))
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

        private bool DistrictsExists(int id)
        {
          return (_context.TblDistrict?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
