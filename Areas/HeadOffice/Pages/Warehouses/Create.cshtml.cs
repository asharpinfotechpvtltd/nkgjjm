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

namespace Nkgjjm.Areas.Panel.Pages.Warehouses
{
    public class CreateModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public CreateModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }
        public List<SelectListItem> ItemMasters { get; set; }
        public List<SelectListItem> District { get; set; }
        public List<ItemMaster> ItemMasterList { get; set; }
        public async Task<IActionResult> OnGet()
        {
            try
            {
                ItemMasterList = await _context.TblItemMaster.ToListAsync();
                ItemMasters = await _context.TblItemMaster.Select(a => new SelectListItem { Text = a.ItemCode.ToString() + "-" + a.ItemName, Value = a.ItemCode.ToString() }).ToListAsync();
                District = await _context.TblDistrict.Select(d => new SelectListItem { Text = d.District, Value = d.id.ToString() }).ToListAsync();

            }
            catch(Exception ex)
            {

            }
            return Page();
        }

        [BindProperty]
        public Warehouse Warehouse { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                GetUserDate date = new GetUserDate();
                var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@District",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = Warehouse.District
                        },
                        new SqlParameter() {
                            ParameterName = "@Block",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = Warehouse.Block
                        },
                        new SqlParameter() {
                            ParameterName = "@Grampanchayat",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = Warehouse.Grampanchayaat
                        },
                        new SqlParameter() {
                            ParameterName = "@VillageCode",
                            SqlDbType =  System.Data.SqlDbType.BigInt,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = Warehouse.VillageCode
                        },
                         new SqlParameter() {
                            ParameterName = "@WarehouseName",
                            SqlDbType =  System.Data.SqlDbType.NVarChar,
                            Direction = System.Data.ParameterDirection.Input,
                            Size = 100,
                            Value = Warehouse.WarehouseName
                        },
                          new SqlParameter() {
                            ParameterName = "@WarehouseId",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Size=100,
                            Direction = System.Data.ParameterDirection.Output
                        } };
                await _context.Database.ExecuteSqlRawAsync("SpAddWareHouse @District,@Block,@Grampanchayat,@VillageCode,@WarehouseName,@WarehouseId out", param);
                string warehouseName = Convert.ToString(param[5].Value);
                string jobworkJSON = Request.Form["jobworkdesc"];
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(jobworkJSON);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][0].ToString()))
                    {
                        Int64 ItemCode = Convert.ToInt64(dt.Rows[i][0]);
                        double OpeningStock = Convert.ToInt64(dt.Rows[i][1]);
                        ItemToWarehouse warehouse = new ItemToWarehouse()
                        {
                            ItemId = ItemCode,
                            WarehouseId = Convert.ToInt32(warehouseName),
                            Qty = OpeningStock,
                            Date = date.ReturnDate()

                        };
                        await _context.TblItemToWarehouse.AddAsync(warehouse);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return RedirectToPage("./Index");
        }
    }
}
