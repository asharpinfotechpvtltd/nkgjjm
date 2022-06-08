using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.GP
{
    public class DetailsModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public DetailsModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

      public GramPanchayats GramPanchayats { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TblGramPanchayat == null)
            {
                return NotFound();
            }

            var grampanchayats = await _context.TblGramPanchayat.FirstOrDefaultAsync(m => m.Id == id);
            if (grampanchayats == null)
            {
                return NotFound();
            }
            else 
            {
                GramPanchayats = grampanchayats;
            }
            return Page();
        }
    }
}
