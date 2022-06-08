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
    public class DetailsModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public DetailsModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
