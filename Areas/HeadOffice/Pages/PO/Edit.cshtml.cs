using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Nkgjjm.Classes;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.PO
{
    public class EditModel : PageModel
    {
        ApplicationDbContext _context;
        public EditModel(ApplicationDbContext context)
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

        public async Task<IActionResult> OnGet(string PoNo)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
            {
                try
                {
                    ItemList = await _context.TblItemMaster.ToListAsync();
                    WarehouseList = await _context.TblWarehouse.ToListAsync();
                    Supplier = await _context.TblSupplier.Select(s => new SelectListItem { Text = s.CompanyName, Value = s.Id.ToString() }).ToListAsync();
                    ItemMasters = await _context.TblItemMaster.Select(a => new SelectListItem { Text = a.ItemCode.ToString(), Value = a.ItemCode.ToString() }).ToListAsync();
                    Warehouse = await _context.TblWarehouse.Select(w => new SelectListItem { Text = w.WarehouseName, Value = w.WarehouseName }).ToListAsync();
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
        public async Task<IActionResult> OnPost(string PoNumber, int suppliername)
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
                        double Price = Convert.ToDouble(dt.Rows[i][2]);
                        string Warehuose = Convert.ToString(dt.Rows[i][3]);
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
            catch (Exception)
            {

            }
            return RedirectToPage("Create");
        }
    }
}