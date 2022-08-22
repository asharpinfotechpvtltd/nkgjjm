using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.WareHouseIncharge.Pages
{
    public class WareHouseDamagedStockModel : PageModel
    {
        ApplicationDbContext _context;
        public WareHouseDamagedStockModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<SPWareHouseDamgedStock> SPWareHouseDamgedStock { get; set; }
        public async Task<IActionResult> OnGet()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
            {
                int Userid = Convert.ToInt32(HttpContext.Session.GetString("Login"));
                var Warehouseid = await _context.TblWarehouseIncharge.SingleOrDefaultAsync(e => e.UserId == Userid);
                var whid = new SqlParameter("@Warehouseid", Warehouseid.WareHouseid);

                SPWareHouseDamgedStock = await _context.SPWareHouseDamgedStock.FromSqlRaw("SPWareHouseDamgedStock @Warehouseid", whid).ToListAsync();
            }
            return Page();
        }
    }
}
