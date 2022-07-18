using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.PO
{
    public class EditModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public EditModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public List<Pochild> Pochild { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            

            var pochild =  await _context.TblPoChild.Where(m => m.Pono == id).ToListAsync();
            
           
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Pochild).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               
            }

            return RedirectToPage("./Index");
        }

        private bool PochildExists(int id)
        {
          return (_context.TblPoChild?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
