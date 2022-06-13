using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.VillageIncharge
{
    public class CreateModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public CreateModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public List<SelectListItem> Districts { get; set; }
        public List<SelectListItem> Warehouse { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Districts = await _context.TblDistrict.Select(a => new SelectListItem { Text = a.District, Value = a.id.ToString() }).ToListAsync();
            Warehouse = await _context.TblWarehouse.Select(a => new SelectListItem { Text = a.WarehouseName, Value = a.Id.ToString() }).ToListAsync();
            return Page();
        }

        [BindProperty]
        public VillageIncharges VillagesIncharge { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.TblVillageCode == null || VillagesIncharge == null)
            {
                return Page();
            }

            _context.TblVillageIncharge.Add(VillagesIncharge);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
