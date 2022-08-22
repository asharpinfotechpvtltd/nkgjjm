using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.GM.Pages.Report
{
    public class StockReserveModel : PageModel
    {
        ApplicationDbContext _context;
        public StockReserveModel(ApplicationDbContext context)
        {
            _context = context;

        }
        public List<SPStockReservation> SPStockReservation { get; set; }
        public List<SelectListItem> WarehouseList { get; set; }
        

        [BindProperty(SupportsGet = true)]
        public DateTime YearStartdate { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime YearEnddate { get; set; }
        [BindProperty(SupportsGet = true)]
        public int warehouse { get; set; }

        public async Task<IActionResult> OnGet()
        {
            WarehouseList = await _context.TblWarehouse.Select(w => new SelectListItem { Text = w.WarehouseName, Value = w.Id.ToString() }).ToListAsync();
            SPStockReservation = await _context.SPStockReservation.FromSqlRaw("SPStockReservation").ToListAsync();
            return Page();
        }
    }
}
