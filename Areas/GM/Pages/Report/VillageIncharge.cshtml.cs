using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.GM.Pages.Report
{
    public class VillageInchargeModel : PageModel
    {
        ApplicationDbContext _context;
        public VillageInchargeModel(ApplicationDbContext context)
        {
            _context = context;

        }

        public List<SPVillageIncharge> VillageInchargeList { get; set; }
        public int TotalVillageIncharge { get; set; }
        public async Task<IActionResult> OnGet()
        {
            VillageInchargeList = await _context.SPVillageIncharge.FromSqlRaw("SPVillageIncharge").ToListAsync();
            TotalVillageIncharge = await _context.TblVillageInchargeForWareHouse.CountAsync();
            return Page();
        }
    }
}
