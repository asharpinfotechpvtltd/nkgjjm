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
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
            {
                try
                {
                    string Email = HttpContext.Session.GetString("Login");
                    var vlgemail = new SqlParameter("@VillageInchargeEmail", Email);
                    SpGetIndentChallan = await _context.SpGetIndentChallan.FromSqlRaw("SpGetIndentChallan @VillageInchargeEmail", vlgemail).ToListAsync();
                }
                catch (Exception ex)
                {

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
