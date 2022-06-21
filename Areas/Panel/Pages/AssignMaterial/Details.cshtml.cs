using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.AssignMaterial
{
    public class DetailsModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public DetailsModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

      public MaterialIssuance MaterialIssuance { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.MaterialIssuance == null)
            {
                return NotFound();
            }

            var materialissuance = await _context.MaterialIssuance.FirstOrDefaultAsync(m => m.id == id);
            if (materialissuance == null)
            {
                return NotFound();
            }
            else 
            {
                MaterialIssuance = materialissuance;
            }
            return Page();
        }
    }
}
