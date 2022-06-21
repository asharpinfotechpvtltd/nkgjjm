using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.Contractor
{
    public class DeleteModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public DeleteModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Contractors Contractors { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TblContractor == null)
            {
                return NotFound();
            }

            var contractors = await _context.TblContractor.FirstOrDefaultAsync(m => m.ContractorId == id);

            if (contractors == null)
            {
                return NotFound();
            }
            else 
            {
                Contractors = contractors;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TblContractor == null)
            {
                return NotFound();
            }
            var contractors = await _context.TblContractor.FindAsync(id);

            if (contractors != null)
            {
                Contractors = contractors;
                _context.TblContractor.Remove(Contractors);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
