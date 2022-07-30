using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nkgjjm.Classes;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.Contractor
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
        public Contractors Contractors { get; set; } = default!;

        GetUserDate date = new GetUserDate();
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.TblContractor == null || Contractors == null)
            {
                return Page();
            }
            Contractors.AddedDate = date.ReturnDate();
            _context.TblContractor.Add(Contractors);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
