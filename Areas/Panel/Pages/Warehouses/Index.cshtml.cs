using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.Panel.Pages.Warehouses
{
    public class IndexModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public IndexModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SPWarehouseList> WarehouseList { get;set; } = default!;
        public int WarehouseCount { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.TblWarehouse != null)
            {
                var warehouse = new SqlParameter("@warehouse", DBNull.Value);
                WarehouseList = await _context.SPWarehouseList.FromSqlRaw("SPWarehouseList @warehouse", warehouse).ToListAsync();
                WarehouseCount = await _context.TblWarehouse.CountAsync();
            }
        }
        public async Task OnPostAsync(string warehouse)
        {
            if (_context.TblWarehouse != null)
            {
                var warehousename = new SqlParameter("@warehouse", warehouse);
                WarehouseList = await _context.SPWarehouseList.FromSqlRaw("SPWarehouseList @warehouse", warehousename).ToListAsync();
                WarehouseCount = WarehouseList.Count;
            }
        }
    }
}
