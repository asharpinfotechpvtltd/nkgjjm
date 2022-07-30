using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.HeadOffice.Pages.VillageIncharge
{
    public class AssignWarehouseToVillageInchargeModel : PageModel
    {
        ApplicationDbContext _context;
        public AssignWarehouseToVillageInchargeModel(ApplicationDbContext context)
        {
            _context = context;

        }
        public List<SelectListItem> WarehouseName { get; set; }
        public List<SelectListItem> VillageInchargeName { get; set; }
        public async Task<IActionResult> OnGet()
        {
            WarehouseName = await _context.TblWarehouse.Select(w => new SelectListItem { Text = w.WarehouseName, Value = w.Id.ToString() }).ToListAsync();
            VillageInchargeName = await _context.TblUser.Where(e=>e.Designation==2).Select(w => new SelectListItem { Text = w.Username, Value = w.Id.ToString() }).ToListAsync();
            return Page();

        }

        public async Task<IActionResult> OnPost(int VillageInchargeName,int WarehouseName)
        {
            VillageIncharges incharges = new VillageIncharges()
            {
                VillageInchargeId = VillageInchargeName,
                WarehouseId = WarehouseName
            };
            await _context.TblVillageInchargeForWareHouse.AddAsync(incharges);
            await _context.SaveChangesAsync();
            return Page();
        }
    }
}
