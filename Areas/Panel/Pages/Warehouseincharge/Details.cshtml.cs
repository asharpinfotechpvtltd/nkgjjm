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
        public PoMaster Pomaster { get; set; }
       
        public DetailsModel(ApplicationDbContext context, IWebHostEnvironment Env)
        {
            _context = context;
            Environmet = Env;
        }

        public List<SPMaterialReceivedCorrespondenceToPo> SPMaterialReceivedCorrespondenceToPo { get; set; } = default!; 
        public string Ponumber { get; set; }
        public async Task<IActionResult> OnGetAsync(string Pono)
        {
            Ponumber = Pono;
            var po = new SqlParameter("@PoId", Pono);
            SPMaterialReceivedCorrespondenceToPo = await _context.SPMaterialReceivedCorrespondenceToPo.FromSqlRaw("SPMaterialReceivedCorrespondenceToPo @PoId", po).ToListAsync(); 
            return Page();
        }
        [BindProperty]
        public IFormFile UploadDoc { get; set; }
        public async Task<IActionResult> OnPostReceiveditem(string Ponumber)
        {
            Upload u = new Upload(Environmet);
            GetUserDate date = new GetUserDate();
            InwardDocument = u.UploadImage(UploadDoc, "InwardDocument");
            InwardDocuments documents = new InwardDocuments()
            {
                Filename = InwardDocument,
                Pono = Ponumber,
                Date = date.ReturnDate()
            };
            await _context.TblInwardDocuments.AddAsync(documents);            
            await _context.SaveChangesAsync();

            Pomaster.Buyer = "NKG";
            Pomaster.Pono = Ponumber;
            await _context.TblPoMaster.AddAsync(Pomaster);
            await _context.SaveChangesAsync();

            string Receiveditem = Request.Form["jobworkdesc"];
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(Receiveditem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dt.Rows[i][1].ToString()))
                {
                    Int64 Productid =Convert.ToInt64(dt.Rows[i][0].ToString());
                    string Qty = Convert.ToString(dt.Rows[i][1]);
                    MaterialReceivedbyPo received = new MaterialReceivedbyPo()
                    {
                        ItemId=Productid,                        
                        RcvdQty = Convert.ToDouble(Qty),
                        PoNo=Ponumber,
                        Date=date.ReturnDate()
                    };
                    await _context.TblMaterialReceivedbyPo.AddAsync(received);
                    await _context.SaveChangesAsync();
                }
            }
            return Page();
        }
    }
}
