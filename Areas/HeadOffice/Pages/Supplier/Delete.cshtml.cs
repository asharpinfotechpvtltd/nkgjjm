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
    public class DeleteModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public DeleteModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Suppliers Suppliers { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
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
            else
            {
                return Redirect("~/Index");
            }
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TblSupplier == null)
            {
                return NotFound();
            }
            var suppliers = await _context.TblSupplier.FindAsync(id);

            if (suppliers != null)
            {
                Suppliers = suppliers;
                _context.TblSupplier.Remove(Suppliers);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
