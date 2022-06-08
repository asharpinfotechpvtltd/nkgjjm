using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.Warehouses
{
    public class DeleteModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public DeleteModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Warehouse Warehouse { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TblWarehouse == null)
            {
                return NotFound();
            }

            var warehouse = await _context.TblWarehouse.FirstOrDefaultAsync(m => m.Id == id);

            if (warehouse == null)
            {
                return NotFound();
            }
            else 
            {
                Warehouse = warehouse;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TblWarehouse == null)
            {
                return NotFound();
            }
            var warehouse = await _context.TblWarehouse.FindAsync(id);

            if (warehouse != null)
            {
                Warehouse = warehouse;
                _context.TblWarehouse.Remove(Warehouse);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
