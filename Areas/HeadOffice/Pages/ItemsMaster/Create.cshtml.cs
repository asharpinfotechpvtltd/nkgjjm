using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Classes;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.ItemsMaster
{
    public class CreateModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public CreateModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            Units = await _context.TblUnits.Select(u => new SelectListItem { Value = u.UnitId.ToString(), Text = u.UnitName }).ToListAsync();
            return Page();
        }
        GetUserDate date = new GetUserDate();
        [BindProperty]
        public ItemMaster ItemMaster { get; set; } = default!;
        public List<SelectListItem> Units { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string? Desc)
        {
            try
            {
                var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@ItemName",
                            SqlDbType =  System.Data.SqlDbType.NVarChar,
                            Size = 1000,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = ItemMaster.ItemName
                        },
                        new SqlParameter() {
                            ParameterName = "@UnitType",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = ItemMaster.UnitType
                        },
                        new SqlParameter() {
                            ParameterName = "@AddedDate",
                            SqlDbType =  System.Data.SqlDbType.DateTime,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = date.ReturnDate()
                        },
                        new SqlParameter() {
                            ParameterName = "@Description",
                            SqlDbType =  System.Data.SqlDbType.NVarChar,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = Desc,
                            Size=100
                        },

                          new SqlParameter() {
                            ParameterName = "@LastId",
                            SqlDbType =  System.Data.SqlDbType.BigInt,
                            Direction = System.Data.ParameterDirection.Output
                        } };
                await _context.Database.ExecuteSqlRawAsync("SPAddItem @ItemName,@UnitType,@AddedDate,@Description,@LastId out", param);
                string Itemcode = Convert.ToString(param[4].Value);

                var warehouselist = await _context.TblWarehouse.ToListAsync();
                if (warehouselist != null)
                {
                    foreach (var item in warehouselist)
                    {
                        ItemToWarehouse warehouse = new ItemToWarehouse()
                        {
                            Date = date.ReturnDate(),
                            ItemId = Convert.ToInt64(Itemcode),
                            Qty = 0,
                            WarehouseId = item.Id
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
