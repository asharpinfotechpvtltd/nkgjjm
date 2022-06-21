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
    public class DeleteModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public DeleteModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.MaterialIssuance == null)
            {
                return NotFound();
            }
            var materialissuance = await _context.MaterialIssuance.FindAsync(id);

            if (materialissuance != null)
            {
                MaterialIssuance = materialissuance;
                _context.MaterialIssuance.Remove(MaterialIssuance);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
