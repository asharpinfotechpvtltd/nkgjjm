using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.Warehouseincharge
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
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
            {
                WareHouseNames = await _context.TblWarehouse.Select(w => new SelectListItem { Text = w.WarehouseName, Value = w.Id.ToString() }).ToListAsync();
                UserList = await _context.TblUser.Where(d => d.Designation == 3).Select(w => new SelectListItem { Text = w.Username, Value = w.Id.ToString() }).ToListAsync();
                return Page();
            }
            else
            {
                return Redirect("~/Index");
            }
        }

        [BindProperty]
        public WarehouseIncharges WarehouseIncharges { get; set; } = default!;
        [BindProperty]

        public List<SelectListItem> WareHouseNames { get; set; }
        public List<SelectListItem> UserList { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                _context.TblWarehouseIncharge.Add(WarehouseIncharges);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }

            return Page();
        }
    }
}
