using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.WareHouseIncharge.Pages
{
    public class VillageInchargeListModel : PageModel
    {
        ApplicationDbContext _context;
        public VillageInchargeListModel(ApplicationDbContext context)
        {
            _context = context;

        }
        public List<SPWareHouseVillageIncharge> SPWareHouseVillageIncharge { get; set; }
        public async Task<IActionResult> OnGet()
        {
            int Userid = Convert.ToInt32(HttpContext.Session.GetString("Login"));
            var Warehouseid = await _context.TblWarehouseIncharge.SingleOrDefaultAsync(e => e.UserId == Userid);
            var whid = new SqlParameter("@Warehouseid", Warehouseid.WareHouseid);
            SPWareHouseVillageIncharge = await _context.SPWareHouseVillageIncharge.FromSqlRaw("SPWareHouseVillageIncharge @Warehouseid", whid).ToListAsync();

            return Page();
        }
    }
}
