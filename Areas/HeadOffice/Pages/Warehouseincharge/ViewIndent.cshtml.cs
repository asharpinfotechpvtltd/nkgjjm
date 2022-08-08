using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.Panel.Pages.Warehouseincharge
{
    public class ViewIndentModel : PageModel
    {
        ApplicationDbContext _context;
        public string searching { get; set; }
        public ViewIndentModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public int IndentMasterid { get; set; }
        public async Task<IActionResult> OnGet(string jobworkid, int IndentMasterId, int WarehouseId)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
            {
                try
                {
                    var search = new SqlParameter("@JobWorkId", jobworkid);
                    var IndentMaster = new SqlParameter("@IndentMasterId", IndentMasterId);
                    var Whid = new SqlParameter("@WarehouseId", WarehouseId);
                    SPMaterialIssuance = await _context.SPMaterialIssuance.FromSqlRaw("SPMaterialIssuance @JobWorkId,@IndentMasterId,@WarehouseId", search, IndentMaster, Whid).ToListAsync();
                    searching = jobworkid;
                    IndentMasterid = IndentMasterId;
                }
                catch (Exception ex)
                {

                }
                return Page();
            }
            else
            {
                return Redirect("~/Index");
            }
        }


        public List<SPMaterialIssuance> SPMaterialIssuance { get; set; }
        public async Task<IActionResult> OnPostSearch(string searchtext, string status)
        {
            try
            {
                IndentMaster warehousestatus = await _context.TblIndentMaster.FirstOrDefaultAsync(e => e.Jobworkid == searchtext);
                if (warehousestatus != null)
                {
                    warehousestatus.WarehouseInchargeStatus = status;
                    await _context.SaveChangesAsync();

                }
                ViewData["Message"] = "Indent Status Updated";

            }
            catch (Exception ex)
            {

            }
            return Page();
        }
    }
}
