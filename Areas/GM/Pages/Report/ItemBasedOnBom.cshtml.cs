using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.GM.Pages.Report
{
    public class ItemBasedOnBomModel : PageModel
    {
        ApplicationDbContext _context;
        public ItemBasedOnBomModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<SpItemBasedOnBom> spItemBasedOnBoms { get; set; }
        public async Task<IActionResult> OnGet()
        {
            spItemBasedOnBoms =await _context.SpItemBasedOnBom.FromSqlRaw("SpItemBasedOnBom").ToListAsync();
            return Page();
        }
    }
}
