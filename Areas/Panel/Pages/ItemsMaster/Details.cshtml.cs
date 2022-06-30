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
    public class DetailsModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public DetailsModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

      public ItemMaster ItemMaster { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TblItemMaster == null)
            {
                return NotFound();
            }

            var itemmaster = await _context.TblItemMaster.FirstOrDefaultAsync(m => m.ItemId == id);
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
    }
}
