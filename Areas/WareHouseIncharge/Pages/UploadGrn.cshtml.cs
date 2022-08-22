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

namespace Nkgjjm.Areas.WareHouseIncharge.Pages.Warehouseincharge
{
    public class IndexModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public IndexModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SPPoList> PoMaster { get; set; } = default!;
        public int TotalPo { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
                {
                    int userid = Convert.ToInt32(HttpContext.Session.GetString("Login"));
                    var Assignedwhid = _context.TblWarehouseIncharge.Where(u => u.UserId == userid);
                    if (_context.TblPoChild != null)
                    {
                        var whid = new SqlParameter("@Warehouseid", Assignedwhid.FirstOrDefault().WareHouseid);
                        PoMaster = await _context.SPPoList.FromSqlRaw("SPPoList @Warehouseid", whid).ToListAsync();
                        TotalPo = PoMaster.Count;
                    }
                    return Page();
                }
                else
                {
                    return Redirect("~/Index");
                }
            }
            catch(Exception )
            {

            }
            return Page();

        }
    }
}
