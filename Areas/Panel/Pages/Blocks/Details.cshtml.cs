using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.Blocks
{
    public class DetailsModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public DetailsModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

      public DistBlock DistBlock { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TblBlock == null)
            {
                return NotFound();
            }

            var distblock = await _context.TblBlock.FirstOrDefaultAsync(m => m.Id == id);
            if (distblock == null)
            {
                return NotFound();
            }
            else 
            {
                DistBlock = distblock;
            }
            return Page();
        }
    }
}
