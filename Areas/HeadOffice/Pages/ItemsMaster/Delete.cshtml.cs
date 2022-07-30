using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.ItemsMaster
{
    public class DeleteModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public DeleteModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public ItemMaster ItemMaster { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TblItemMaster == null)
            {
                return NotFound();
            }

            var itemmaster = await _context.TblItemMaster.FirstOrDefaultAsync(m => m.ItemCode == id);

            if (itemmaster == null)
            {
                return NotFound();
            }
            else 
            {
                ItemMaster = itemmaster;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TblItemMaster == null)
            {
                return NotFound();
            }
            var itemmaster = await _context.TblItemMaster.FindAsync(id);

            if (itemmaster != null)
            {
                ItemMaster = itemmaster;
                _context.TblItemMaster.Remove(ItemMaster);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
