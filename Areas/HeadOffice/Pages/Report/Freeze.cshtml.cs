using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.HeadOffice.Pages.Report
{
    public class FreezeModel : PageModel
    {
        ApplicationDbContext _context;
        public FreezeModel(ApplicationDbContext context)
        {
            _context = context;

        }

        public List<SpFreezedReport> SpFreezedReport { get; set; }

        public List<SelectListItem> WarehouseList { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime YearStartdate { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime YearEnddate { get; set; }
        [BindProperty(SupportsGet = true)]
        public int warehouse { get; set; }


        public async Task<IActionResult> OnGet(DateTime startdate, DateTime enddate, string submit)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
            {
                if (string.IsNullOrEmpty(submit))
                {
                    YearStartdate = new DateTime(2021, 2, 10);
                    YearEnddate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    int warehouseid = 0;
                    var warehouses = new SqlParameter("@WareHouse", warehouseid);
                    var startdates = new SqlParameter("@startdate", YearStartdate);
                    var enddates = new SqlParameter("@enddate", YearEnddate);
                    SpFreezedReport = await _context.SpFreezedReport.FromSqlRaw("SpFreezedReport @Startdate,@Enddate,@WareHouse", startdates, enddates, warehouses).ToListAsync();

                }
                else
                {
                    if (warehouse == 0)
                    {
                        YearStartdate = startdate;
                        YearEnddate = enddate;
                        var warehouses = new SqlParameter("@WareHouse", warehouse);
                        var startdates = new SqlParameter("@startdate", startdate);
                        var enddates = new SqlParameter("@enddate", enddate);
                        SpFreezedReport = await _context.SpFreezedReport.FromSqlRaw("SpFreezedReport @Startdate,@Enddate,@WareHouse", startdates, enddates, warehouses).ToListAsync();


                    }
                    else if (warehouse > 0)
                    {
                        YearStartdate = startdate;
                        YearEnddate = enddate;
                        var warehouses = new SqlParameter("@WareHouse", warehouse);
                        var startdates = new SqlParameter("@startdate", startdate);
                        var enddates = new SqlParameter("@enddate", enddate);
                        SpFreezedReport = await _context.SpFreezedReport.FromSqlRaw("SpFreezedReport @Startdate,@Enddate,@WareHouse", startdates, enddates, warehouses).ToListAsync();
                    }
                }

                WarehouseList = await _context.TblWarehouse.Select(w => new SelectListItem { Text = w.WarehouseName, Value = w.Id.ToString() }).ToListAsync();



                return Page();

            }
            else
            {
                return Redirect("~/Index");
            }
        }


    }
}
