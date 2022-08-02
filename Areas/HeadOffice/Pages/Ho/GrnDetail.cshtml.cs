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
    public class GrnDetailModel : PageModel
    {
        ApplicationDbContext _context;

        public GrnDetailModel(ApplicationDbContext context)
        {
            _context = context;

        }
        [BindProperty]
        public PoVehicleDetail PoVehicledetail { get; set; }
        public int warehouse_id { get; set; }
        public string WarehouseName { get; set; }
     

        
        public List<SPMaterialReceivedCorrespondenceToPo> SPMaterialReceivedCorrespondenceToPo { get; set; }
        public async Task<IActionResult> OnGet(string Pono, string challan,int warehouseid)
        {
            try
            {
                var po = new SqlParameter("@PONo", Pono);
                var chln = new SqlParameter("@Challannumber", challan);
                var whid = new SqlParameter("@Warehouseid", warehouseid);
                warehouse_id = warehouseid;
                SPMaterialReceivedCorrespondenceToPo = await _context.SPMaterialReceivedCorrespondenceToPo.FromSqlRaw("SPMaterialReceivedCorrespondenceToPo @PONo,@Challannumber,@Warehouseid", po, chln, whid).ToListAsync();
                PoVehicledetail = await _context.TblPoVehicleDetail.FirstOrDefaultAsync(c => c.PoNo == Pono && c.ChallanNumber == challan);
                var Whname = await _context.TblWarehouse.FirstOrDefaultAsync(e => e.Id == warehouseid);
                WarehouseName = Whname.WarehouseName;
            }
            catch(Exception ex)
            {

            }

            
            return Page();
        }

        public async Task<IActionResult> OnPost(int warehouse,string txttransactionno,string status,string txtreason,string Ponumber)
        {
            try
            {
                GetUserDate date = new GetUserDate();
                string Receiveditem = Request.Form["jobworkdesc"];
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(Receiveditem);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][1].ToString()))
                    {
                        Int64 Productid = Convert.ToInt64(dt.Rows[i][0].ToString());
                        string Qty = Convert.ToString(dt.Rows[i][1]);
                        var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@ItemCode",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = Productid
                        },
                        new SqlParameter() {
                            ParameterName = "@Warehouseid",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = warehouse
                        },
                        new SqlParameter() {
                            ParameterName = "@Qty",
                            SqlDbType =  System.Data.SqlDbType.Float,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = Qty
                        },new SqlParameter() {
                            ParameterName ="@date" ,
                            SqlDbType =  System.Data.SqlDbType.Date,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = date.ReturnDate()
                        } };

                        await _context.Database.ExecuteSqlRawAsync("SpUpdateQty @Warehouseid,@ItemCode,@Qty,@date", param);
                        



                    }
                }
                var postatus =  _context.TblMaterialReceivedbyPo.Where(po => po.PoNo == Ponumber);
                if(postatus!=null)
                {
                    foreach(var item in postatus)
                    {
                        item.Status = status;
                        item.TransactionId = txttransactionno;
                        item.Reason = txtreason;
                    }
                }
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {

            }
            return RedirectToPage("GrnMaster");
        }
    }
}
