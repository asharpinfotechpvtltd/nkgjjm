using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.GM.Pages.JobWorks
{
    public class IndexModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public IndexModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SPJobWorkList> JobWork { get; set; } = default!;

        [BindProperty]
        public int TotalJobWork { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
            {
                if (_context.TblJobWork != null)
                {
                    JobWork = await _context.SPJobWorkList.FromSqlRaw("SPJobWorkList").ToListAsync();
                    TotalJobWork = await _context.TblJobWork.CountAsync();
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
