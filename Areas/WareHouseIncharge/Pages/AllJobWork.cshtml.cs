using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.WareHouseIncharge.Pages
{
    public class AllJobWorkModel : PageModel
    {
        ApplicationDbContext _context;
        public AllJobWorkModel(ApplicationDbContext context)
        {
            _context = context;

        }
        [BindProperty(SupportsGet = true)]
        public DateTime YearStartdate { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime YearEnddate { get; set; }
        [BindProperty(SupportsGet = true)]
        public int warehouse { get; set; }

        public List<SPAssignedJobWorkToWareHouse> SPAssignedJobWorkToWareHouse { get; set; }
        public async Task<IActionResult> OnGet()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
            {
                var wh_id = await _context.TblWarehouseIncharge.FirstAsync(e=>e.UserId==Convert.ToInt32( HttpContext.Session.GetString("Login")));
                int whid = wh_id.WareHouseid;
                var Warehouseid = new SqlParameter("@WareHouseId", whid);
                SPAssignedJobWorkToWareHouse = await _context.SPAssignedJobWorkToWareHouse.FromSqlRaw("SPAssignedJobWorkToWareHouse @WareHouseId", Warehouseid).ToListAsync();
                return Page();
            }
            else
            {
                return Redirect("~/Index");
            }

        }
    }
}
