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
    public class DeleteModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public DeleteModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TblGramPanchayat == null)
            {
                return NotFound();
            }
            var grampanchayats = await _context.TblGramPanchayat.FindAsync(id);

            if (grampanchayats != null)
            {
                GramPanchayats = grampanchayats;
                _context.TblGramPanchayat.Remove(GramPanchayats);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
