using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.District
{
    public class DeleteModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public DeleteModel(Nkgjjm.Models.ApplicationDbContext context)
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

            var districts = await _context.TblDistrict.FirstOrDefaultAsync(m => m.id == id);

            if (districts == null)
            {
                return NotFound();
            }
            else 
            {
                Districts = districts;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TblDistrict == null)
            {
                return NotFound();
            }
            var districts = await _context.TblDistrict.FindAsync(id);

            if (districts != null)
            {
                Districts = districts;
                _context.TblDistrict.Remove(Districts);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
