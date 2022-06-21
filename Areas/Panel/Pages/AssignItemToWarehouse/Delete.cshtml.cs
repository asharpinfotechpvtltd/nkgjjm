using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.AssignItemToWarehouse
{
    public class DeleteModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public DeleteModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public ItemToWarehouse ItemToWarehouse { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TblItemToWarehouse == null)
            {
                return NotFound();
            }

            var itemtowarehouse = await _context.TblItemToWarehouse.FirstOrDefaultAsync(m => m.Id == id);

            if (itemtowarehouse == null)
            {
                return NotFound();
            }
            else 
            {
                ItemToWarehouse = itemtowarehouse;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TblItemToWarehouse == null)
            {
                return NotFound();
            }
            var itemtowarehouse = await _context.TblItemToWarehouse.FindAsync(id);

            if (itemtowarehouse != null)
            {
                ItemToWarehouse = itemtowarehouse;
                _context.TblItemToWarehouse.Remove(ItemToWarehouse);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
