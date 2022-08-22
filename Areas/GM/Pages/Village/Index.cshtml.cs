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

namespace Nkgjjm.Areas.GM.Pages.Village
{
    public class IndexModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public IndexModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SPVillageList> VillageList { get; set; } = default!;
        public int TotalVillageCount { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
            {
                if (_context.TblVillageCode != null)
                {
                    var village = new SqlParameter("@Village", DBNull.Value);
                    VillageList = await _context.SPVillageList.FromSqlRaw("SPVillageList @Village", village).ToListAsync();
                    TotalVillageCount = await _context.TblVillageCode.CountAsync();
                }
                return Page();
            }
            else
            {
                return Redirect("~/Index");
            }
        }
    
    public async Task OnPostAsync(string Village)
    {
        if (_context.TblVillageCode != null)
        {
            var village = new SqlParameter("@Village", Village);
            VillageList = await _context.SPVillageList.FromSqlRaw("SPVillageList @Village", village).ToListAsync();
            TotalVillageCount = VillageList.Count;
        }
    }
}
}
