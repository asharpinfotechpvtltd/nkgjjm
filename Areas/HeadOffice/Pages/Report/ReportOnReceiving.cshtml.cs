using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.HeadOffice.Pages.Report
{
    public class ReportOnReceivingModel : PageModel
    {
        ApplicationDbContext _context;
        public ReportOnReceivingModel(ApplicationDbContext context)
        {
            _context = context;

        }
        public List<SPReportOnReceiving> SPReportOnReceiving { get; set; }

        public List<SelectListItem> PoLists { get; set; }
        public List<SelectListItem> SupplierLists { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime YearStartdate { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime YearEnddate { get; set; }
        
        
        


        public async Task<IActionResult> OnGet(DateTime startdate, DateTime enddate)
        {
            PoLists = await _context.TblPoMaster.Select(i => new SelectListItem
            {
                Text = i.Pono,
                Value = i.Pono.ToString()
            }).ToListAsync();

            YearStartdate = new DateTime(2022, 4, 1);
            YearEnddate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);            
            SupplierLists = await _context.TblSupplier.Select(w => new SelectListItem { Text = w.CompanyName, Value = w.Id.ToString() }).ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnGetSearch(DateTime startdate, DateTime enddate, string PoList, int SupplierList)
        {
            if (PoList == null && SupplierList == 0)
            {
                YearStartdate = startdate;
                YearEnddate = enddate;
                var SupplierName = new SqlParameter("@SupplierName", SupplierList);
                var po = new SqlParameter("@Pono", PoList);
                var startdates = new SqlParameter("@startdate", startdate);
                var enddates = new SqlParameter("@enddate", enddate);
                SPReportOnReceiving = await _context.SPReportOnReceiving.FromSqlRaw("SPReportOnReceiving @SupplierName,@Pono,@Startdate,@Enddate", SupplierName, po, startdates, enddates).ToListAsync();


            }
            else if (PoList != null && SupplierList > 0)
            {
                YearStartdate = startdate;
                YearEnddate = enddate;
                var SupplierName = new SqlParameter("@SupplierName", SupplierList);
                var po = new SqlParameter("@Pono", PoList);
                var startdates = new SqlParameter("@startdate", startdate);
                var enddates = new SqlParameter("@enddate", enddate);
                SPReportOnReceiving = await _context.SPReportOnReceiving.FromSqlRaw("SPReportOnReceiving @SupplierName,@Pono,@Startdate,@Enddate", SupplierName, po, startdates, enddates).ToListAsync();
            }
            else if (PoList!=null)
            {
                YearStartdate = startdate;
                YearEnddate = enddate;
                var SupplierName = new SqlParameter("@SupplierName", SupplierList);
                var po = new SqlParameter("@Pono", PoList);
                var startdates = new SqlParameter("@startdate", startdate);
                var enddates = new SqlParameter("@enddate", enddate);
                SPReportOnReceiving = await _context.SPReportOnReceiving.FromSqlRaw("SPReportOnReceiving @SupplierName,@Pono,@Startdate,@Enddate", SupplierName, po, startdates, enddates).ToListAsync();
            }
            else if (SupplierList > 0)
            {
                YearStartdate = startdate;
                YearEnddate = enddate;
                var SupplierName = new SqlParameter("@SupplierName", SupplierList);
                var po = new SqlParameter("@Pono", PoList);
                var startdates = new SqlParameter("@startdate", startdate);
                var enddates = new SqlParameter("@enddate", enddate);
                SPReportOnReceiving = await _context.SPReportOnReceiving.FromSqlRaw("SPReportOnReceiving @SupplierName,@Pono,@Startdate,@Enddate", SupplierName, po, startdates, enddates).ToListAsync();
            }

            PoLists = await _context.TblPoMaster.Select(i => new SelectListItem
            {
                Text = i.Pono,
                Value = i.Pono.ToString()
            }).ToListAsync();
            SupplierLists = await _context.TblSupplier.Select(w => new SelectListItem { Text = w.CompanyName, Value = w.Id.ToString() }).ToListAsync();
            return Page();
        }
    }
}
