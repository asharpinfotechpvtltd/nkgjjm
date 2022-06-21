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

namespace Nkgjjm.Areas.Panel.Pages.AssignItemToWarehouse
{
    public class CreateModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public CreateModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }
        public List<SelectListItem> WarehouseList { get; set; }
        public List<SelectListItem> ItemList { get; set; }
        GetUserDate date = new GetUserDate();
        public async Task<IActionResult> OnGet()
        {
            WarehouseList =await _context.TblWarehouse.Select(a => new SelectListItem { Text = a.WarehouseName, Value = a.Id.ToString() }).ToListAsync();
            ItemList = await _context.TblItemMaster.Select(a => new SelectListItem { Text = a.ItemName, Value = a.ItemId.ToString() }).ToListAsync();
            return Page();
        }

        [BindProperty]
        public ItemToWarehouse ItemToWarehouse { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.TblItemToWarehouse == null || ItemToWarehouse == null)
            {
                return Page();
            }
            ItemToWarehouse.Date = date.ReturnDate();
            _context.TblItemToWarehouse.Add(ItemToWarehouse);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
