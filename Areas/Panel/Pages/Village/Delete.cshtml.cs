using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.Village
{
    public class DeleteModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public DeleteModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Villages Villages { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TblVillageCode == null)
            {
                return NotFound();
            }

            var villages = await _context.TblVillageCode.FirstOrDefaultAsync(m => m.Id == id);

            if (villages == null)
            {
                return NotFound();
            }
            else 
            {
                Villages = villages;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TblVillageCode == null)
            {
                return NotFound();
            }
            var villages = await _context.TblVillageCode.FindAsync(id);

            if (villages != null)
            {
                Villages = villages;
                _context.TblVillageCode.Remove(Villages);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
