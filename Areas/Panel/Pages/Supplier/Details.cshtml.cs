using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.Supplier
{
    public class DetailsModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public DetailsModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

      public Suppliers Suppliers { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TblSupplier == null)
            {
                return NotFound();
            }

            var suppliers = await _context.TblSupplier.FirstOrDefaultAsync(m => m.Id == id);
            if (suppliers == null)
            {
                return NotFound();
            }
            else 
            {
                Suppliers = suppliers;
            }
            return Page();
        }
    }
}
