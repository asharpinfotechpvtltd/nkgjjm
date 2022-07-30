using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public CreateModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }
        public List<SelectListItem> Designation { get; set; }
        public async Task<IActionResult> OnGet()
        {
            Designation = await _context.TblDesignation.Select(d => new SelectListItem { Text = d.DesignationName, Value = d.Id.ToString() }).ToListAsync();
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int Designation)
        {

            User.Designation = Designation;
            User.Status = true;
            _context.TblUser.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
