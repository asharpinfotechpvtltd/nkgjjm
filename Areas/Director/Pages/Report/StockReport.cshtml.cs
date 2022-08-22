using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.Director.Pages.Report
{
    public class StockReportModel : PageModel
    {
        ApplicationDbContext _context;
        public StockReportModel(ApplicationDbContext context)
        {
            _context = context;

        }
        public List<SelectListItem> WarehouseList { get; set; }
        public List<SpSearchWarehouseStock> SpSearchWarehouseStock { get; set; }
        public async Task<IActionResult> OnGet()
        {
            WarehouseList = await _context.TblWarehouse.Select(w => new SelectListItem { Text = w.WarehouseName, Value = w.Id.ToString() }).ToListAsync();
            var warehouse = new SqlParameter("@warehouseid", DBNull.Value);
            var startdate = new SqlParameter("@startdate", DBNull.Value);
            var enddate = new SqlParameter("@enddate", DBNull.Value);
            SpSearchWarehouseStock = await _context.SpSearchWarehouseStock.FromSqlRaw("SpSearchWarehouseStock @warehouseid,@startdate,@enddate", warehouse, startdate, enddate).ToListAsync();
            return Page();

        }
        public async Task<IActionResult> OnPost(int warehouse, DateTime startdate, DateTime enddate)
        {
            WarehouseList = await _context.TblWarehouse.Select(w => new SelectListItem { Text = w.WarehouseName, Value = w.Id.ToString() }).ToListAsync();
            if (warehouse == 0)
            {

                var warehouses = new SqlParameter("@warehouseid", warehouse);
                var startdates = new SqlParameter("@startdate", startdate.Date);
                var enddates = new SqlParameter("@enddate", enddate.Date);
                SpSearchWarehouseStock = await _context.SpSearchWarehouseStock.FromSqlRaw("SpSearchWarehouseStock @warehouseid,@startdate,@enddate", warehouses, startdates, enddates).ToListAsync();
            }
            else
            {
                var warehouses = new SqlParameter("@warehouseid", warehouse);
                var startdates = new SqlParameter("@startdate", startdate.Date);
                var enddates = new SqlParameter("@enddate", enddate.Date);
                SpSearchWarehouseStock = await _context.SpSearchWarehouseStock.FromSqlRaw("SpSearchWarehouseStock @warehouseid,@startdate,@enddate", warehouses, startdates, enddates).ToListAsync();
            }

            return Page();

        }
    }
}
