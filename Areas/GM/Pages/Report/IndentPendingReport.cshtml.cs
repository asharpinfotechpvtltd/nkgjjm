using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.GM.Pages.Report
{
    public class IndentPendingReportModel : PageModel
    {
        public List<SpIndentPendingReport> SpIndentPendingReport { get; set; }
        ApplicationDbContext _context;
        public IndentPendingReportModel(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<IActionResult> OnGet()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
            {
                SpIndentPendingReport = await _context.SpIndentPendingReport.FromSqlRaw("SpIndentPendingReport").ToListAsync();
                return Page();
            }
            else
            {
                return Redirect("~/Index");
            }
        }
    }
}
