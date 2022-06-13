using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.VillageIncharge
{
    public class DeleteModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public DeleteModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public VillageIncharges VillageIncharges { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TblVillageIncharge == null)
            {
                return NotFound();
            }

            var villageincharges = await _context.TblVillageIncharge.FirstOrDefaultAsync(m => m.Id == id);

            if (villageincharges == null)
            {
                return NotFound();
            }
            else 
            {
                VillageIncharges = villageincharges;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TblVillageIncharge == null)
            {
                return NotFound();
            }
            var villageincharges = await _context.TblVillageIncharge.FindAsync(id);

            if (villageincharges != null)
            {
                VillageIncharges = villageincharges;
                _context.TblVillageIncharge.Remove(VillageIncharges);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
