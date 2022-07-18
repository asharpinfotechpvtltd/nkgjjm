using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Nkgjjm.Classes;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;
using System.Data;

namespace Nkgjjm.Areas.Panel.Pages.VillageIncharge
{
    public class CreateIndentModel : PageModel
    {
        ApplicationDbContext _context;
        public string searching { get; set; }
        public CreateIndentModel(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<IActionResult> OnGet()
        {

            return Page();
        }

        public List<SPMaterialIssuance> SPMaterialIssuance { get; set; }
        public async Task<IActionResult> OnPostSearch(string searchtext)
        {
            var search = new SqlParameter("@JobWorkId", searchtext);
            SPMaterialIssuance = await _context.SPMaterialIssuance.FromSqlRaw("SPMaterialIssuance @JobWorkId", search).ToListAsync();
            searching = searchtext;
            HttpContext.Session.SetString("Jobworkid", searchtext);
            return Page();
        }
        public async Task<IActionResult> OnPostCreateIndent()
        {
            GetUserDate date = new GetUserDate();
            string ViId = "VI001";
            string jobworkJSON = Request.Form["jobworkdesc"];
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(jobworkJSON);
            string Jobworkid = HttpContext.Session.GetString("Jobworkid");
            if (dt.Rows.Count > 0)
            {
                IndentMaster indentMaster = new IndentMaster()
                {
                    Date = date.ReturnDate(),
                    Jobworkid = Jobworkid,
                    VillageInchargeEmail = ViId
                };
                await _context.TblIndentMaster.AddAsync(indentMaster);
                await _context.SaveChangesAsync();


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][1].ToString()))
                    {
                        Indent issuance = new Indent()
                        {
                            ItemCode = Convert.ToInt64(dt.Rows[i][1]),
                            Jobworkid = Convert.ToString(dt.Rows[i][0]),
                            Demand = Convert.ToDouble(dt.Rows[i][2]),
                            Status = "Pending"

                        };
                        await _context.TblIndent.AddAsync(issuance);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            return Page();
        }
    }
}
