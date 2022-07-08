using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
            return Page();
        }
        public async Task<IActionResult> OnPostCreateIndent()
        {
            string ViId = "VI001";
            string jobworkJSON = Request.Form["jobworkdesc"];
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(jobworkJSON);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][1].ToString()))
                    {
                        MaterialIssuance issuance = new MaterialIssuance()
                        {
                            ItemId = Convert.ToInt32(dt.Rows[i][1].ToString()),
                            JobWorkId = dt.Rows[i][0].ToString(),
                            MaterialPickedqty = Convert.ToDouble(dt.Rows[i][2].ToString()),
                            VillageIncharge=ViId
                        };
                        await _context.TblMaterialIssuance.AddAsync(issuance);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            return Page();
        }
    }
}
