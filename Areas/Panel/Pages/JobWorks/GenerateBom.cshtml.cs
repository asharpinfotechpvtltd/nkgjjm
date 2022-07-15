using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Nkgjjm.Classes;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;
using System.Data;

namespace Nkgjjm.Areas.Panel.Pages.JobWorks
{
    public class GenerateBomModel : PageModel
    {
        ApplicationDbContext _context;
        public GenerateBomModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Bom Bom { get; set; }
        public List<SPBomList> ListBom { get; set; }
        public List<SelectListItem> ItemMasters { get; set; }
        public List<ItemMaster> ItemList { get; set; }
        public string JobWorkid { get; set; }
        
        public async Task<IActionResult> OnGet(string id)
        {
            JobWorkid = id;
            ItemList = await _context.TblItemMaster.ToListAsync();
            // var Jobid = new SqlParameter("@JOBWORKID", id);
            ItemMasters = await _context.TblItemMaster.Select(a => new SelectListItem { Text = a.ItemName, Value = a.ItemCode.ToString() }).ToListAsync();
            //ListBom = await _context.SPBomList.FromSqlRaw("SPBomList @JOBWORKID",Jobid).ToListAsync();

            return Page();
        }
        public async Task<IActionResult> OnPost(string JobWorkId)
        {
            Bom.JobWorkId = JobWorkId;
            GetUserDate date = new GetUserDate();
            string jobworkJSON = Request.Form["jobworkdesc"];
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(jobworkJSON);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dt.Rows[i][1].ToString()))
                {
                    string Productid = dt.Rows[i][0].ToString();
                    string Qty = Convert.ToString(dt.Rows[i][1]);
                    Bom desc = new Bom()
                    {
                        AssignedDate= date.ReturnDate(),
                        JobWorkId=JobWorkId,
                        RawMaterialId=Convert.ToInt64(Productid),
                        Qty=Convert.ToDouble(Qty)
                     };
                    await _context.TblBom.AddAsync(desc);
                    await _context.SaveChangesAsync();
                }
            }      
            return Redirect("Index");
        }
    }
}
