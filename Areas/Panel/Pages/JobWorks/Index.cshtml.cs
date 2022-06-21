using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.Panel.Pages.JobWorks
{
    public class IndexModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public IndexModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SPJobWorkList> JobWork { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.TblJobWork != null)
            {
                JobWork = await _context.SPJobWorkList.FromSqlRaw("SPJobWorkList").ToListAsync();
            }
        }
    }
}
