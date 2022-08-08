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
        public List<SpWareHouseTransferPendingListForWareHouse> SpWareHouseTransferPendingListForWareHouse { get; set; }
        public async Task<IActionResult> OnGet()
        {
            int Warehouseid = 10;
            var Whid = new SqlParameter("@Fromwarehouseid", Warehouseid);
            SpWareHouseTransferPendingListForWareHouse = await _context.SpWareHouseTransferPendingListForWareHouse.FromSqlRaw("SpWareHouseTransferPendingListForWareHouse @Fromwarehouseid",Whid).ToListAsync();
            return Page();
        }
    }
}
