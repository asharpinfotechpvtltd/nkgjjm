using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.VillageIncharge.Pages.VillageIncharge
{
    public class IndentStatusModel : PageModel
    {
        ApplicationDbContext _context;
        public IndentStatusModel(ApplicationDbContext context)
        {
            _context = context;

        }
        public List<SpGetIndentChallan> SpGetIndentChallan { get; set; }
        public async Task<IActionResult> OnGet()
        {
            string Email = HttpContext.Session.GetString("Login");
            var vlgemail = new SqlParameter("@VillageInchargeEmail", Email);
            SpGetIndentChallan = await _context.SpGetIndentChallan.FromSqlRaw("SpGetIndentChallan @VillageInchargeEmail", vlgemail).ToListAsync();
            return Page();
        }
    }
}
