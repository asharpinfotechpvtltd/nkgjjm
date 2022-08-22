using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.HeadOffice.Pages.Report
{
    public class OverAllStockModel : PageModel
    {
        ApplicationDbContext _context;
        public OverAllStockModel(ApplicationDbContext context)
        {
            _context = context;

        }

        public List<SpReportByAll> SpReportByAll { get; set; }

        public List<SelectListItem> WarehouseList { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime YearStartdate { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime YearEnddate { get; set; }
        [BindProperty(SupportsGet = true)]
        public int warehouse { get; set; }
        public List<SelectListItem> ProductList { get; set; }


        public async Task<IActionResult> OnGet(DateTime startdate, DateTime enddate)
        {
            ProductList = await _context.TblItemMaster.Select(i => new SelectListItem
            {
                Text = i.ItemName,
                Value = i.ItemCode.ToString()
            }).ToListAsync();

            YearStartdate = new DateTime(2022, 4, 1);
            YearEnddate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var warehouses = new SqlParameter("@WareHouse", DBNull.Value);
            var startdates = new SqlParameter("@startdate", DBNull.Value);
            var enddates = new SqlParameter("@enddate", DBNull.Value);
            SpReportByAll = await _context.SpReportByAll.FromSqlRaw("SpReportByAll @Startdate,@Enddate,@WareHouse", startdates, enddates, warehouses).ToListAsync();
            WarehouseList = await _context.TblWarehouse.Select(w => new SelectListItem { Text = w.WarehouseName, Value = w.Id.ToString() }).ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnGetSearch(DateTime startdate, DateTime enddate, Int64 Productid, int Warehouseid)
        {
            if (Warehouseid == 0 && Productid == 0)
            {
                YearStartdate = startdate;
                YearEnddate = enddate;
                var warehouses = new SqlParameter("@WareHouse", Warehouseid);
                var startdates = new SqlParameter("@startdate", startdate);
                var enddates = new SqlParameter("@enddate", enddate);
                SpReportByAll = await _context.SpReportByAll.FromSqlRaw("SpReportByAll @Startdate,@Enddate,@WareHouse", startdates, enddates, warehouses).ToListAsync();


            }
            else if (Warehouseid > 0 && Productid > 0)
            {
                YearStartdate = startdate;
                YearEnddate = enddate;
                var warehouses = new SqlParameter("@WareHouse", Warehouseid);
                var startdates = new SqlParameter("@startdate", startdate);
                var enddates = new SqlParameter("@enddate", enddate);
                var proid = new SqlParameter("@Itemcode", Productid);
                SpReportByAll = await _context.SpReportByAll.FromSqlRaw("SpReportByAll @Startdate,@Enddate,@WareHouse,@Itemcode", startdates, enddates, warehouses, proid).ToListAsync();
            }
            else if (Warehouseid > 0)
            {
                YearStartdate = startdate;
                YearEnddate = enddate;
                var warehouses = new SqlParameter("@WareHouse", Warehouseid);
                var startdates = new SqlParameter("@startdate", startdate);
                var enddates = new SqlParameter("@enddate", enddate);
                var proid = new SqlParameter("@Itemcode", Productid);
                SpReportByAll = await _context.SpReportByAll.FromSqlRaw("SpReportByAll @Startdate,@Enddate,@WareHouse,@Itemcode", startdates, enddates, warehouses,proid).ToListAsync();
            }
            else if (Productid > 0)
            {
                YearStartdate = startdate;
                YearEnddate = enddate;
                var warehouses = new SqlParameter("@WareHouse", Warehouseid);
                var startdates = new SqlParameter("@startdate", startdate);
                var enddates = new SqlParameter("@enddate", enddate);
                var proid = new SqlParameter("@Itemcode", Productid);
                SpReportByAll = await _context.SpReportByAll.FromSqlRaw("SpReportByAll @Startdate,@Enddate,@WareHouse,@Itemcode", startdates, enddates, warehouses, proid).ToListAsync();

            }

                ProductList = await _context.TblItemMaster.Select(i => new SelectListItem
            {
                Text = i.ItemName,
                Value = i.ItemCode.ToString()
            }).ToListAsync();
            WarehouseList = await _context.TblWarehouse.Select(w => new SelectListItem { Text = w.WarehouseName, Value = w.Id.ToString() }).ToListAsync();
            return Page();
        }
    }
}
