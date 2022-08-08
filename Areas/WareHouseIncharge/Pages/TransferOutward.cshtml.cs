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

namespace Nkgjjm.Areas.WareHouseIncharge.Pages
{
    public class TransferOutwardModel : PageModel
    {
        ApplicationDbContext _context;
        public TransferOutwardModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<SelectListItem> Warehouse { get; set; }
        public List<SPWareHouseStock> SPWareHouseStock { get; set; }
        public int LoggedInWarehouseid { get; set; }
        public async Task<IActionResult> OnGet()
        {

            Warehouse = await _context.TblWarehouse.Select(w => new SelectListItem { Text = w.WarehouseName, Value = w.Id.ToString() }).ToListAsync();
            LoggedInWarehouseid = 10;
            HttpContext.Session.SetString("Warehouse", LoggedInWarehouseid.ToString());
            var warehouseid = new SqlParameter("@WareHouseId", LoggedInWarehouseid);
            SPWareHouseStock = await _context.SPWareHouseStock.FromSqlRaw("SPWareHouseStock @WareHouseId", warehouseid).ToListAsync();
            return Page();

        }
        public async Task<IActionResult> OnPost(int Warehouse)
        {
            try
            {
                GetUserDate date = new GetUserDate();
                string ViId = HttpContext.Session.GetString("Login");
                int warehouseid = Convert.ToInt32(HttpContext.Session.GetString("Warehouseid"));
                string jobworkJSON = Request.Form["jobworkdesc"];
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(jobworkJSON);
                int whrid = Convert.ToInt32(HttpContext.Session.GetString("Warehouse"));
                if (Warehouse != whrid)
                {

                    var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@FromWareHouseid",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = whrid
                        },
                        new SqlParameter() {
                            ParameterName = "@ToWareHouseid",
                            SqlDbType =  System.Data.SqlDbType.NVarChar,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = Warehouse
                        },
                        new SqlParameter() {
                            ParameterName = "@AddedDate",
                            SqlDbType =  System.Data.SqlDbType.Date,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = date.ReturnDate()
                        },
                          new SqlParameter() {
                            ParameterName = "@Tbltransferwarehousechild",
                            SqlDbType =  System.Data.SqlDbType.Structured,
                            TypeName="dbo.WarehouseTransferChild",
                            Value=dt

                        } };
                    await _context.Database.ExecuteSqlRawAsync("SPWarehouseTransferMaster @FromWareHouseid,@ToWareHouseid,@AddedDate,@Tbltransferwarehousechild", param);
                    ViewData["Message"] = "created";
                }
            }
            catch (Exception ex)
            {

            }
            return Page();
        }
    }
}
