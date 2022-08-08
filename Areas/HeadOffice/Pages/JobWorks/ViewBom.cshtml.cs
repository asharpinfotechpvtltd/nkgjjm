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
    public class ViewBomModel : PageModel
    {
        ApplicationDbContext _context;
        public ViewBomModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<SelectListItem> District { get; set; }
        public List<SelectListItem> Contractor { get; set; }
        public List<SelectListItem> JobWorkCategory { get; set; }
        public List<SelectListItem> ItemMaster { get; set; }
        [BindProperty]
        public string JobWorkid { get; set; }

        public List<SPBomDetail> Bom { get; set; }
        public async Task<IActionResult> OnGet(string id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
            {
                JobWorkid = id;
                District = await _context.TblDistrict.Select(a => new SelectListItem { Text = a.District, Value = a.id.ToString() }).ToListAsync();
                Contractor = await _context.TblContractor.Select(a => new SelectListItem { Text = a.ContractorName, Value = a.ContractorId.ToString() }).ToListAsync();
                JobWorkCategory = await _context.TblJobWorkCategory.Select(a => new SelectListItem { Text = a.JobWorkcategory, Value = a.Id.ToString() }).ToListAsync();
                ItemMaster = await _context.TblItemMaster.Select(a => new SelectListItem { Text = a.ItemName, Value = a.ItemCode.ToString() }).ToListAsync();
                var jobworkid = new SqlParameter("@JobWorkId", id);
                Bom = await _context.SPBomDetail.FromSqlRaw("SPBomDetail @JobWorkId", jobworkid).ToListAsync();
                return Page();
            }
            else
            {
                return Redirect("~/Index");
            }
        }

        [BindProperty]
        public JobWork JobWork { get; set; } = default!;

        GetUserDate date = new GetUserDate();
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {


            var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@District",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = JobWork.District
                        },
                        new SqlParameter() {
                            ParameterName = "@Block",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Scale = 100,
                            Value = JobWork.Block
                        },


                        new SqlParameter() {
                            ParameterName = "@GramPanchaayat",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Size=100,
                            Direction = System.Data.ParameterDirection.Input,
                             Scale = 100,
                            Value = JobWork.GramPanchaayat
                        },
                        new SqlParameter() {
                            ParameterName = "@VillageCode",
                            SqlDbType =  System.Data.SqlDbType.BigInt,
                            Size=100,
                            Direction = System.Data.ParameterDirection.Input,
                             Scale = 100,
                            Value = JobWork.VillageCode
                        },
                        new SqlParameter() {
                            ParameterName = "@ContractorId",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Size=100,
                            Direction = System.Data.ParameterDirection.Input,
                             Scale = 100,
                            Value = JobWork.ContractorId
                        },
                        new SqlParameter() {
                            ParameterName = "@JobWorkDesc",
                            SqlDbType =  System.Data.SqlDbType.NVarChar,
                            Size=100,
                            Direction = System.Data.ParameterDirection.Input,
                             Scale = 100,
                            Value = "JobWork.JobWorkDesc"
                        },
                        new SqlParameter() {
                            ParameterName = "@JobWorkCategory",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Size=100,
                            Direction = System.Data.ParameterDirection.Input,
                             Scale = 100,
                            Value = JobWork.JobWorkCategory
                        },
                         new SqlParameter() {
                            ParameterName = "@Date",
                            SqlDbType =  System.Data.SqlDbType.Date,
                            Size=100,
                            Direction = System.Data.ParameterDirection.Input,
                             Scale = 100,
                            Value = date.ReturnDate()
                        },
                         new SqlParameter() {
                            ParameterName = "@Unit",
                            SqlDbType =  System.Data.SqlDbType.NVarChar,
                            Size=100,
                            Direction = System.Data.ParameterDirection.Input,
                             Scale = 100,
                            Value = "JobWork.Unit"
                        },
                          new SqlParameter() {
                            ParameterName = "@Rate",
                            SqlDbType =  System.Data.SqlDbType.Float,
                            Size=100,
                            Direction = System.Data.ParameterDirection.Input,
                             Scale = 100,
                            Value = 100
                        },
                          new SqlParameter() {
                            ParameterName = "@OutJobid",
                            SqlDbType =  System.Data.SqlDbType.NVarChar,
                            Size=100,
                            Direction = System.Data.ParameterDirection.Input,
                            Scale=100,
                            Value=JobWork.WorkorderId

                        }
            };


            _context.Database.ExecuteSqlRaw("SPJobWork @District,@Block ,@GramPanchaayat,@VillageCode,@ContractorId,@JobWorkDesc," +
                "@JobWorkCategory,@Date,@Unit,@Rate,@OutJobid", param);
            string JobWorkid = Convert.ToString(param[10].Value);

            string jobworkJSON = Request.Form["jobworkdesc"];
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(jobworkJSON);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dt.Rows[i][2].ToString()))
                {
                    string Particular = dt.Rows[i][0].ToString();
                    string Unit = Convert.ToString(dt.Rows[i][1]);
                    double Rate = Convert.ToDouble(dt.Rows[i][2].ToString());
                    JobDescription desc = new JobDescription();
                    desc.Unit = Unit;
                    desc.Particular = Particular;
                    desc.Rate = Rate;
                    desc.JobWorkid = JobWork.WorkorderId;
                    await _context.TblJobDescription.AddAsync(desc);
                    await _context.SaveChangesAsync();

                }
            }
            return RedirectToPage("./Index");
        }
    }
}