using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.Panel.Pages.Ho
{
    public class ClosureReportModel : PageModel
    {
        ApplicationDbContext _context;
        public ClosureReportModel(ApplicationDbContext context)
        {
            _context = context;

        }
        public List<SpHoClosure> SpHoClosure { get; set; }
        public async Task<IActionResult> OnGet()
        {
            SpHoClosure =await _context.SpHoClosure.FromSqlRaw("SpHoClosure").ToListAsync();
            return Page();
        }
    }
}
