using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Nkgjjm.Classes;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;
using System.Data;

namespace Nkgjjm.Areas.VillageIncharge.Pages.VillageIncharge
{
    public class CreateIndentModel : PageModel
    {
        ApplicationDbContext _context;
        public string searching { get; set; }
        public CreateIndentModel(ApplicationDbContext context)
        {
            _context = context;

        }
        public IActionResult OnGet()
        {

            return Page();
        }

        public List<SPCreateIndent> SPMaterialIssuance { get; set; }
        public async Task<IActionResult> OnPostSearch(string searchtext)
        {
            try
            {
                int Userid = Convert.ToInt32(HttpContext.Session.GetString("Login"));
                var search = new SqlParameter("@JobWorkId", searchtext);
                SPMaterialIssuance = await _context.SPCreateIndent.FromSqlRaw("SPCreateIndent @JobWorkId", search).ToListAsync();
                searching = searchtext;
                HttpContext.Session.SetString("Jobworkid", searchtext);
                VillageIncharges incharges = await _context.TblVillageInchargeForWareHouse.FirstOrDefaultAsync(e => e.VillageInchargeId == Userid);
                if (incharges != null)
                {
                    int warehouseid = incharges.WarehouseId;
                    HttpContext.Session.SetString("Warehouseid", warehouseid.ToString());
                    HttpContext.Session.SetString("Userid", Userid.ToString());
                }
            }
            catch(Exception ex)
            {

            }
            return Page();
        }
        public async Task<IActionResult> OnPostCreateIndent()
        {
            try
            {
                GetUserDate date = new GetUserDate();
                string ViId = HttpContext.Session.GetString("Login");
                int warehouseid = Convert.ToInt32(HttpContext.Session.GetString("Warehouseid"));
                string jobworkJSON = Request.Form["jobworkdesc"];
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(jobworkJSON);
                string Jobworkid = HttpContext.Session.GetString("Jobworkid");
                if (dt.Rows.Count > 0)
                {

                    var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@Jobworkid",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = Jobworkid
                        },
                        new SqlParameter() {
                            ParameterName = "@VillageInchargeEmail",
                            SqlDbType =  System.Data.SqlDbType.NVarChar,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = ViId
                        },
                        new SqlParameter() {
                            ParameterName = "@WareHouseid",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = warehouseid
                        },
                        new SqlParameter() {
                            ParameterName = "@Date",
                            SqlDbType =  System.Data.SqlDbType.Date,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = date.ReturnDate()
                        },
                          new SqlParameter() {
                            ParameterName = "@IndentMasterId",
                            SqlDbType =  System.Data.SqlDbType.BigInt,
                            Size=100,
                            Direction = System.Data.ParameterDirection.Output
                        } };
                    await _context.Database.ExecuteSqlRawAsync("SPIndentMaster @Jobworkid,@VillageInchargeEmail,@WareHouseid,@Date,@IndentMasterId out", param);
                    string IndentMasterid = Convert.ToString(param[4].Value);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][2].ToString()))
                        {
                            Indent issuance = new Indent()
                            {
                                ItemCode = Convert.ToInt64(dt.Rows[i][1]),
                                Jobworkid = Convert.ToString(dt.Rows[i][0]),
                                Demand = Convert.ToDouble(dt.Rows[i][2]),
                                Status = "Pending",
                                IndentMasterid = Convert.ToInt64(IndentMasterid)

                            };
                            await _context.TblIndent.AddAsync(issuance);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                ViewData["Message"] = "Indent Created";
            }
            catch(Exception ex)
            {

            }
            return Page();
        }
    }
}
