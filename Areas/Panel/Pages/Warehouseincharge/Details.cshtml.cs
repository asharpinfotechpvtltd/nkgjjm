using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Nkgjjm.Classes;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.Panel.Pages.Warehouseincharge
{
    public class DetailsModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;
        IWebHostEnvironment Environmet;
        
        public string InwardDocument { get; set; }

        [BindProperty]
        public PoVehicleDetail PoVehicleDetail { get; set; }
       
        public DetailsModel(ApplicationDbContext context, IWebHostEnvironment Env)
        {
            _context = context;
            Environmet = Env;
        }

        public List<SPMaterialReceivedCorrespondenceToPo> SPMaterialReceivedCorrespondenceToPo { get; set; } = default!; 
        public string Ponumber { get; set; }
        public int Warehouseid { get; set; }
        public async Task<IActionResult> OnGetAsync(string Pono)
        {
            WarehouseIncharges WarehouseIncharge = await _context.TblWarehouseIncharge.SingleOrDefaultAsync(e => e.Emailid == "karan@gmail.com");
            if (WarehouseIncharge != null)
            {
                 Warehouseid = WarehouseIncharge.WareHouseid;
            }
            Ponumber = Pono;
            var po = new SqlParameter("@PoId", Pono);
            var challan = new SqlParameter("@Challannumber", DBNull.Value);
            var Whid = new SqlParameter("@Warehouseid", Warehouseid);
            SPMaterialReceivedCorrespondenceToPo = await _context.SPMaterialReceivedCorrespondenceToPo.FromSqlRaw("SPMaterialReceivedCorrespondenceToPo @PoId,@Challannumber,@Warehouseid", po,challan,Whid).ToListAsync(); 
            return Page();
        }
        [BindProperty]
        public IFormFile UploadDoc { get; set; }
        public async Task<IActionResult> OnPostReceiveditem(string Ponumber,int warehousename)
        {
            Upload u = new Upload(Environmet);           
            InwardDocument = u.UploadImage(UploadDoc, "InwardDocument");
            PoVehicleDetail.PoNo = Ponumber;
            PoVehicleDetail.SupportedDocument = InwardDocument;
            await _context.TblPoVehicleDetail.AddAsync(PoVehicleDetail);
            

            string Receiveditem = Request.Form["jobworkdesc"];
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(Receiveditem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dt.Rows[i][1].ToString()))
                {
                    Int64 Productid =Convert.ToInt64(dt.Rows[i][0].ToString());
                    string Qty = Convert.ToString(dt.Rows[i][1]);
                    double Challanqty = Convert.ToDouble(dt.Rows[i][2]);
                    MaterialReceivedbyPo received = new MaterialReceivedbyPo()
                    {
                        ItemId=Productid,                        
                        RcvdQty = Convert.ToDouble(Qty),
                        PoNo=Ponumber,
                        Challanqty=Challanqty,
                        Challan_Invoicenumber= PoVehicleDetail.ChallanNumber,
                        Warehouse=warehousename
                    };
                    await _context.TblMaterialReceivedbyPo.AddAsync(received);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToPage("Index");
        }
    }
}
