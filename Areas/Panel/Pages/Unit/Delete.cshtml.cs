using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.Unit
{
    public class DeleteModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public DeleteModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Units Units { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TblUnits == null)
            {
                return NotFound();
            }

            var units = await _context.TblUnits.FirstOrDefaultAsync(m => m.UnitId == id);

            if (units == null)
            {
                return NotFound();
            }
            else 
            {
                Units = units;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TblUnits == null)
            {
                return NotFound();
            }
            var units = await _context.TblUnits.FindAsync(id);

            if (units != null)
            {
                Units = units;
                _context.TblUnits.Remove(Units);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
