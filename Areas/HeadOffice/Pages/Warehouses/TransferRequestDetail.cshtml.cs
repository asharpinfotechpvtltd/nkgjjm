using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Classes;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.HeadOffice.Pages.Warehouses
{
    public class TransferRequestDetailModel : PageModel
    {
        ApplicationDbContext _context;
        public string searching { get; set; }
        IWebHostEnvironment Environmet;
        [BindProperty]
        public IFormFile challan { get; set; }
        public string challanName { get; set; }


        public TransferRequestDetailModel(ApplicationDbContext context, IWebHostEnvironment Env)
        {
            _context = context;
            Environmet = Env;
        }
        
        public List<SpWarehouseTransferChildForHo> SpWarehouseTransferChildForHo { get; set; }
        [BindProperty]
        public int TransferMasterId { get; set; }
        
        public async Task<IActionResult> OnGet(int masterId)
        {
            TransferMasterId = masterId;
            var MasterId = new SqlParameter("@TransferMasterid", masterId);
            SpWarehouseTransferChildForHo = await _context.SpWarehouseTransferChildForHo.FromSqlRaw("SpWarehouseTransferChildForHo @TransferMasterid",MasterId).ToListAsync();
            return Page();
        }
        public async Task<IActionResult> OnPostUploadchallan(int masterid)
        {
            try
            {
                Upload u = new Upload(Environmet);
                challanName = u.UploadImage(challan, "TransferChallan");
                WarehouseTransfer warehousestatus = await _context.TblWarehouseTransfer.FirstOrDefaultAsync(e => e.TransferMasterid == masterid);
                
                if (warehousestatus != null)
                {
                    warehousestatus.HoStatus = "Approved";
                    warehousestatus.Challan = challanName;
                }
                await _context.SaveChangesAsync();
                ViewData["Message"] = "Challan Updated";
            }
            catch (Exception ex)
            {

            }

            return Page();
        }
    }
}
