using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.WareHouseIncharge.Pages
{
    public class TransferOutwardMasterModel : PageModel
    {
        ApplicationDbContext _context;

        public TransferOutwardMasterModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public WarehouseIncharges Wi { get; set; }
        public List<SpWareHouseTransferPendingListForWareHouse> SpWareHouseTransferPendingListForWareHouse { get; set; }
        public async Task<IActionResult> OnGet()
        {
            int Userid = Convert.ToInt32(HttpContext.Session.GetString("Login"));
            Wi = await _context.TblWarehouseIncharge.FirstOrDefaultAsync(e => e.UserId == Userid);
            if (Wi != null)
            {
                var Whid = new SqlParameter("@Fromwarehouseid", Wi.WareHouseid);
                SpWareHouseTransferPendingListForWareHouse = await _context.SpWareHouseTransferPendingListForWareHouse.FromSqlRaw("SpWareHouseTransferPendingListForWareHouse @Fromwarehouseid", Whid).ToListAsync();
            }
            return Page();
        }
    }
}
