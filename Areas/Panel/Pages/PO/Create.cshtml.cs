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
    public class CreateModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public CreateModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            Product = await _context.TblItemMaster.Select(p => new SelectListItem { Text = p.ItemName, Value = p.ItemId.ToString() }).ToListAsync();
            return Page();
        }

        [BindProperty]
        public Pochild Pochild { get; set; } = default!;
        public List<SelectListItem> Product { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.TblPoChild == null || Pochild == null)
            {
                return Page();
            }

            _context.TblPoChild.Add(Pochild);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
