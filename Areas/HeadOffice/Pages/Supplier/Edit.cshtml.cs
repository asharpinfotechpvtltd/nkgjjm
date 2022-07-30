using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.Supplier
{
    public class EditModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public EditModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Suppliers Suppliers { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TblSupplier == null)
            {
                return NotFound();
            }

            var suppliers =  await _context.TblSupplier.FirstOrDefaultAsync(m => m.Id == id);
            if (suppliers == null)
            {
                return NotFound();
            }
            Suppliers = suppliers;
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

            _context.Attach(Suppliers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuppliersExists(Suppliers.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SuppliersExists(int id)
        {
          return (_context.TblSupplier?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
