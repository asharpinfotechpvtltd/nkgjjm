using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nkgjjm.Classes;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.Supplier
{
    public class CreateModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public CreateModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Suppliers Suppliers { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                GetUserDate date = new GetUserDate();
                if (!ModelState.IsValid || _context.TblSupplier == null || Suppliers == null)
                {
                    return Page();
                }
                Suppliers.AddedDate = date.ReturnDate();
                _context.TblSupplier.Add(Suppliers);
                await _context.SaveChangesAsync();
            } catch(Exception ex)
            {

            }
            
            return RedirectToPage("./Index");
        }
    }
}
