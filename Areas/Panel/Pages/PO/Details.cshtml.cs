using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.PO
{
    public class DetailsModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public DetailsModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

      public Pochild Pochild { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TblPoChild == null)
            {
                return NotFound();
            }

            var pochild = await _context.TblPoChild.FirstOrDefaultAsync(m => m.Id == id);
            if (pochild == null)
            {
                return NotFound();
            }
            else 
            {
                Pochild = pochild;
            }
            return Page();
        }
    }
}
