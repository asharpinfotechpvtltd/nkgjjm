using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.WareHouseIncharge.Pages
{
    public class UpdateTransferStatusModel : PageModel
    {
        ApplicationDbContext _context;
        public UpdateTransferStatusModel(ApplicationDbContext context)
        {
            _context = context;

        }
        [BindProperty]
        public WarehouseTransfer WarehouseTransfer { get; set; }
        public List<SpWarehouseTransferChildForHo> SpWarehouseTransferChildForHo { get; set; }
        public async Task<IActionResult> OnGet(int TransferMasterId)
        {
            WarehouseTransfer = await _context.TblWarehouseTransfer.FirstOrDefaultAsync(e => e.TransferMasterid == TransferMasterId);
            var Masterid = new SqlParameter("@TransferMasterid", TransferMasterId);
            SpWarehouseTransferChildForHo = await _context.SpWarehouseTransferChildForHo.FromSqlRaw("SpWarehouseTransferChildForHo @TransferMasterid", Masterid).ToListAsync();
            return Page();

        }
        public async Task<IActionResult> OnPost(int masterid)
        {
            var status = await _context.TblWarehouseTransfer.FirstOrDefaultAsync(e => e.TransferMasterid == masterid);
            if (status != null)
            {
                status.BiltyNo = WarehouseTransfer.BiltyNo;
                status.DriverName = WarehouseTransfer.DriverName;
                status.MobileNo = WarehouseTransfer.MobileNo;
                status.VehicleNo = WarehouseTransfer.VehicleNo;
                status.FromWareHouseStatus = "Approved";
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("TransferOutwardMaster");
        }
    }
}
