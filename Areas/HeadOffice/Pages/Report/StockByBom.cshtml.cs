using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.HeadOffice.Pages.Report
{
    public class StockByBomModel : PageModel
    {
        public List<SPStockByBom> SPStockByBom { get; set; }
        ApplicationDbContext _context;
        public StockByBomModel(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<IActionResult> OnGet()
        {
            SPStockByBom = await _context.SPStockByBom.FromSqlRaw("SPStockByBom").ToListAsync();
            return Page();
        }
    }
}
