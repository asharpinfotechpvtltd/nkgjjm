using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.Designations
{
    public class DeleteModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public DeleteModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Designation Designation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TblDesignation == null)
            {
                return NotFound();
            }

            var designation = await _context.TblDesignation.FirstOrDefaultAsync(m => m.Id == id);

            if (designation == null)
            {
                return NotFound();
            }
            else 
            {
                Designation = designation;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TblDesignation == null)
            {
                return NotFound();
            }
            var designation = await _context.TblDesignation.FindAsync(id);

            if (designation != null)
            {
                Designation = designation;
                _context.TblDesignation.Remove(Designation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
