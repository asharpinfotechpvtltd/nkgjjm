using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.Panel.Pages.Warehouses
{
    public class StockModel : PageModel
    {
        ApplicationDbContext _context;
        public StockModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<SpWarehouseStocklog> Warehouselog { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            var warehouseid = new SqlParameter("@warehouseid", id);
            Warehouselog =await _context.SpWarehouseStocklog.FromSqlRaw("SpWarehouseStocklog @warehouseid", warehouseid).ToListAsync();
            return Page();
        }
    }
}
