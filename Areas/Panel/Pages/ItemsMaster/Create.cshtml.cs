using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Classes;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.ItemsMaster
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
            Units =await _context.TblUnits.Select(u=>new SelectListItem { Value=u.UnitId.ToString(),Text=u.UnitName}).ToListAsync();
            return Page();
        }
        GetUserDate date = new GetUserDate();
        [BindProperty]
        public ItemMaster ItemMaster { get; set; } = default!;
        public List<SelectListItem> Units { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            ItemMaster.AddedDate = date.ReturnDate();
            _context.TblItemMaster.Add(ItemMaster);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
