using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Classes;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.VillageIncharge.Pages.VillageIncharge
{
    public class UploadChallanModel : PageModel
    {
        ApplicationDbContext _context;
        public string searching { get; set; }
        IWebHostEnvironment Environmet;
        [BindProperty]
        public IFormFile challan { get; set; }
        public string challanName { get; set; }


        public UploadChallanModel(ApplicationDbContext context, IWebHostEnvironment Env)
        {
            _context = context;
            Environmet = Env;
        }
        public string jobid { get; set; }

        public int IndentMasterid { get; set; }
        public IActionResult OnGet(string jobworkid, int IndentMasterId)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
            {
                jobid = jobworkid;
                IndentMasterid = IndentMasterId;
                return Page();
            }
            else
            {
                return Redirect("~/Index");
            }


        }





        public async Task<IActionResult> OnPost(string jobid, int IndentMasterid)
        {
            try
            {
                Upload u = new Upload(Environmet);
                challanName = u.UploadImage(challan, "Challan");
                IndentMaster warehousestatus = await _context.TblIndentMaster.FirstOrDefaultAsync(e => e.Jobworkid == jobid);
                var indentchallan = await _context.TblIndentChallan.FirstOrDefaultAsync(e => e.Jobworkid == jobid && e.IndentMasterId == IndentMasterid);
                if (indentchallan != null)
                {
                    indentchallan.VillageinchargeFile = challanName;
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
