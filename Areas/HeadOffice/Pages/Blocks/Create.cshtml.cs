using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.Blocks
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
            District =await _context.TblDistrict.Select(a => new SelectListItem { Text = a.District, Value = a.id.ToString() }).ToListAsync();
            return Page();
        }

        [BindProperty]
        public DistBlock DistBlock { get; set; } = default!;

        [BindProperty]
        public List<SelectListItem> District { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          

          await  _context.TblBlock.AddAsync(DistBlock);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
