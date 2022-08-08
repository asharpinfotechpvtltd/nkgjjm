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

namespace Nkgjjm.Areas.HeadOffice.Pages.JobWorks
{
    public class EditBomModel : PageModel
    {
        ApplicationDbContext _context;
        public EditBomModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Bom Bom { get; set; }
        public List<SPBomDetail> ListBom { get; set; }
        public List<SelectListItem> ItemMasters { get; set; }
        public List<ItemMaster> ItemList { get; set; }
        public string JobWorkid { get; set; }
        public List<JobDescription> JobDescription { get; set; }
        public List<SelectListItem> JobDescriptionList { get; set; }

        public async Task<IActionResult> OnGet(string id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
            {
                try
                {
                    JobWorkid = id;
                    ItemList = await _context.TblItemMaster.ToListAsync();
                    ItemMasters = await _context.TblItemMaster.Select(a => new SelectListItem { Text = a.ItemName, Value = a.ItemCode.ToString() }).ToListAsync();
                    JobDescriptionList = await _context.TblJobDescription.Where(e => e.JobWorkid == JobWorkid).Select(a => new SelectListItem { Text = a.Particular, Value = a.id.ToString() }).ToListAsync();
                    JobDescription = await _context.TblJobDescription.Where(j => j.JobWorkid == JobWorkid).ToListAsync();
                    var jobid = new SqlParameter("@JobWorkId", JobWorkid);
                    ListBom = await _context.SPBomDetail.FromSqlRaw("SPBomDetail @JobWorkId", jobid).ToListAsync();
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
        public async Task<IActionResult> OnPost(string JobWorkId)
        {
            try
            {
                Bom.JobWorkId = JobWorkId;
                var bom = _context.TblBom.Where(e => e.JobWorkId == Bom.JobWorkId);
                if (bom != null)
                {
                    _context.TblBom.RemoveRange(bom);
                }
                await _context.SaveChangesAsync();

                GetUserDate date = new GetUserDate();
                string jobworkJSON = Request.Form["jobworkdesc"];
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(jobworkJSON);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][1].ToString()))
                    {
                        string Productid = dt.Rows[i][0].ToString();
                        string Deviation = dt.Rows[i][1].ToString();
                        string FinalQty = Convert.ToString(dt.Rows[i][2]);
                        Bom desc = new Bom()
                        {
                            AssignedDate = date.ReturnDate(),
                            JobWorkId = JobWorkId,
                            RawMaterialId = Convert.ToInt64(Productid),
                            Qty = Convert.ToDouble(FinalQty),
                            Deviation = Convert.ToDouble(Deviation),
                        };
                        await _context.TblBom.AddAsync(desc);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Redirect("Index");
        }
    }
}