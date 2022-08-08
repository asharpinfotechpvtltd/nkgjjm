using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
            {
                if (_context.TblPoChild != null)
                {
                    PoMaster = await _context.SPPoList.FromSqlRaw("SPPoList").ToListAsync();
                    TotalPo = await _context.TblPoMaster.CountAsync();
                }
                return Page();
            }
            else
            {
                return Redirect("~/Index");
            }

        }
    }
}
