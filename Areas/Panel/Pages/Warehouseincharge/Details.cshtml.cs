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
    public class DetailsModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public DetailsModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
