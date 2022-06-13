using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.Warehouseincharge
{
    public class DeleteModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public DeleteModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public WarehouseIncharges WarehouseIncharges { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TblWarehouseIncharge == null)
            {
                return NotFound();
            }

            var warehouseincharges = await _context.TblWarehouseIncharge.FirstOrDefaultAsync(m => m.id == id);

            if (warehouseincharges == null)
            {
                return NotFound();
            }
            else 
            {
                WarehouseIncharges = warehouseincharges;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TblWarehouseIncharge == null)
            {
                return NotFound();
            }
            var warehouseincharges = await _context.TblWarehouseIncharge.FindAsync(id);

            if (warehouseincharges != null)
            {
                WarehouseIncharges = warehouseincharges;
                _context.TblWarehouseIncharge.Remove(WarehouseIncharges);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
