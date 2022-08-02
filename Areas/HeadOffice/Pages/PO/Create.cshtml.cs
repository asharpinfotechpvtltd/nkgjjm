using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Nkgjjm.Classes;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.Panel.Pages.PO
{
    public class CreateModel : PageModel
    {
        ApplicationDbContext _context;
        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }
     
        public List<SelectListItem> Supplier { get; set; }
        [BindProperty]
        public List<SelectListItem> ItemMasters { get; set; }
        [BindProperty]
        public List<SelectListItem> Warehouse { get; set; }
        public List<ItemMaster> ItemList { get; set; }
        public List<Warehouse> WarehouseList { get; set; }
        public string JobWorkid { get; set; }

        public async Task<IActionResult> OnGet()
        {
            try
            {


                ItemList = await _context.TblItemMaster.ToListAsync();
                WarehouseList = await _context.TblWarehouse.ToListAsync();
                Supplier = await _context.TblSupplier.Select(s => new SelectListItem { Text = s.CompanyName, Value = s.Id.ToString() }).ToListAsync();
                ItemMasters = await _context.TblItemMaster.Select(a => new SelectListItem { Text = a.ItemCode.ToString() + "-" + a.ItemName, Value = a.ItemCode.ToString() }).ToListAsync();
                Warehouse = await _context.TblWarehouse.Select(w => new SelectListItem { Text = w.WarehouseName, Value = w.Id.ToString() }).ToListAsync();
            }
            catch(Exception ex)
            {

            }
            return Page();
        }
        public async Task<IActionResult> OnPost(string PoNumber,int suppliername)
        {
            try
            {
                GetUserDate date = new GetUserDate();
                PoMaster pomaster = new PoMaster()
                {
                    Buyer = "Nkg Infrastructure",
                    date = date.ReturnDate(),
                    Pono = PoNumber,
                    Supplier = suppliername

                };
                await _context.TblPoMaster.AddAsync(pomaster);
                await _context.SaveChangesAsync();

                string Po = Request.Form["jobworkdesc"];
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(Po);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][1].ToString()))
                    {
                        Int64 Productid = Convert.ToInt64(dt.Rows[i][0].ToString());
                        double Qty = Convert.ToDouble(dt.Rows[i][1]);
                        string Warehuose = Convert.ToString(dt.Rows[i][2]);
                        Pochild child = new Pochild()
                        {
                            Pono = PoNumber,
                            Itemid = Productid,
                            Qty = Qty,
                            WarehouseName = Warehuose
                        };
                        await _context.TblPoChild.AddAsync(child);
                        await _context.SaveChangesAsync();
                    }
                }
            }
             catch(Exception)
            { }
            ViewData["Message"] = string.Format("Po Created");
            return Page();
        }
    }
}