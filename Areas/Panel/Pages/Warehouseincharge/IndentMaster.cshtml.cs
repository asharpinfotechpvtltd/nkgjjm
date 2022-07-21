using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.Panel.Pages.Warehouseincharge
{
    public class IndentMasterModel : PageModel
    {
        ApplicationDbContext _context;
        public IndentMasterModel(ApplicationDbContext context)
        {
            _context = context;

        }
        [BindProperty]
        public List<SPIndentMasterForWarehouseIncharge> IndentMaster { get; set; }
        public WarehouseIncharges Wi { get; set; }
        public async Task<IActionResult> OnGet()
        {
            Wi = await _context.TblWarehouseIncharge.SingleOrDefaultAsync(e => e.Emailid == "karan@gmail.com");
            var Warehouseid = new SqlParameter("@Warehouseid", Wi.WareHouseid);

            IndentMaster =await _context.SPIndentMasterForWarehouseIncharge.FromSqlRaw("SPIndentMasterForWarehouseIncharge @Warehouseid", Warehouseid).ToListAsync();
            return Page();
        }
    }
}
