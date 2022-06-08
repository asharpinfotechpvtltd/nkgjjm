using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.District
{
    public class DetailsModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public DetailsModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

      public Districts Districts { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TblDistrict == null)
            {
                return NotFound();
            }

            var districts = await _context.TblDistrict.FirstOrDefaultAsync(m => m.id == id);
            if (districts == null)
            {
                return NotFound();
            }
            else 
            {
                Districts = districts;
            }
            return Page();
        }
    }
}
