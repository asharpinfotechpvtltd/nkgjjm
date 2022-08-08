using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Nkgjjm.Classes;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;
using System.Data;

namespace Nkgjjm.Areas.Panel.Pages.Ho
{
    public class ViewIndentModel : PageModel
    {
        ApplicationDbContext _context;
        public string searching { get; set; }
        IWebHostEnvironment Environmet;
        [BindProperty]
        public IFormFile challan { get; set; }
        public string challanName { get; set; }


        public ViewIndentModel(ApplicationDbContext context, IWebHostEnvironment Env)
        {
            _context = context;
            Environmet = Env;
        }

        public int IndentMasterid { get; set; }
        public int W_hid { get; set; }
        public async Task<IActionResult> OnGet(string jobworkid, int IndentMasterId, int WarehouseId)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
            {
                try
                {
                    W_hid = WarehouseId;
                    var search = new SqlParameter("@JobWorkId", jobworkid);
                    var IndentMaster = new SqlParameter("@IndentMasterId", IndentMasterId);
                    var Whid = new SqlParameter("@WarehouseId", WarehouseId);
                    SPValidateByHo = await _context.SPValidateByHo.FromSqlRaw("SPValidateByHo @JobWorkId,@IndentMasterId,@WarehouseId", search, IndentMaster, Whid).ToListAsync();
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

        public List<SPValidateByHo> SPValidateByHo { get; set; }



        public async Task<IActionResult> OnPostSearch(string searchtext, string status, int IndentMasterid, int Whid)
        {
            try
            {
                Upload u = new Upload(Environmet);
                challanName = u.UploadImage(challan, "Challan");
                IndentMaster warehousestatus = await _context.TblIndentMaster.FirstOrDefaultAsync(e => e.Jobworkid == searchtext);
                IndentChallan Hochallan = new IndentChallan()
                {
                    Hofile = challanName,
                    IndentMasterId = IndentMasterid,
                    Jobworkid = searchtext

                };
                await _context.TblIndentChallan.AddAsync(Hochallan);
                if (warehousestatus != null)
                {
                    warehousestatus.WarehouseInchargeStatus = status;
                    await _context.SaveChangesAsync();
                }

                GetUserDate date = new GetUserDate();
                string ViId = HttpContext.Session.GetString("Login");
                int warehouseid = Convert.ToInt32(HttpContext.Session.GetString("Warehouseid"));

                string jobworkJSON = Request.Form["indent"];
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(jobworkJSON);
                string Jobworkid = HttpContext.Session.GetString("Jobworkid");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][1].ToString()))
                        {
                            var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@WarehouseId",
                            SqlDbType =  System.Data.SqlDbType.NVarChar,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = Whid
                        },
                        new SqlParameter() {
                            ParameterName = "@ItemCode",
                            SqlDbType =  System.Data.SqlDbType.BigInt,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = dt.Rows[i][0].ToString()
                        },
                        new SqlParameter() {
                            ParameterName = "@Qty",
                            SqlDbType =  System.Data.SqlDbType.Float,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = dt.Rows[i][1].ToString()
                        },
                        new SqlParameter() {
                            ParameterName = "@Date",
                            SqlDbType =  System.Data.SqlDbType.Date,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = date.ReturnDate()
                        },
                        new SqlParameter() {
                            ParameterName = "@Status",
                            SqlDbType =  System.Data.SqlDbType.NVarChar,
                            Direction = System.Data.ParameterDirection.Input,
                            Size=100,
                            Value = status
                        },
                        new SqlParameter() {
                            ParameterName = "@IndentMasterId",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Size = 100,
                            Value = IndentMasterid
                        },
                        new SqlParameter() {
                            ParameterName = "@Jobworkid",
                            SqlDbType =  System.Data.SqlDbType.NVarChar,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = searchtext
                        } };

                            await _context.Database.ExecuteSqlRawAsync("SpUpdateQtyInIndent @WareHouseid,@ItemCode,@Qty,@Date,@Status,@IndentMasterId,@Jobworkid", param);




                        }
                    }
                }
            }
            catch (Exception ex) { }


            ViewData["Message"] = "Indent Status Updated";
            return RedirectToPage("~/IndentMaster");
        }
    }
}
