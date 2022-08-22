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

namespace Nkgjjm.Areas.Panel.Pages.PO
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
        public IList<SPPoSearch> SpPoSearch { get; set; }
        public async Task<IActionResult> OnGetSearch(string PoNo)
        {
            var Po = new SqlParameter("@PoNo", PoNo);
            SpPoSearch = await _context.SPPoSearch.FromSqlRaw("SPPoSearch @PoNo", Po).ToListAsync();

            return Page();
        }
    }
}
