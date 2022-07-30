using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.JobWorkCategory
{
    public class IndexModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public IndexModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public int TotalCount { get; set; }
        public IList<JobWorkcategories> JobWorkcategories { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.TblJobWorkCategory != null)
            {
                JobWorkcategories = await _context.TblJobWorkCategory.ToListAsync();
                TotalCount = await _context.TblJobWorkCategory.CountAsync();
            }
        }
    }
}
