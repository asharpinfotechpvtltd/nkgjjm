using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.Panel.Pages.Warehouseincharge
{
    public class IndentMasterModel : PageModel
    {
        ApplicationDbContext _context;
        public IndentMasterModel(ApplicationDbContext context)
        {
            _context = context;

        }
        [BindProperty]
        public List<SPIndentMaster> IndentMaster { get; set; }
        public async Task<IActionResult> OnGet()
        {
            IndentMaster =await _context.SPIndentMaster.FromSqlRaw("SPIndentMaster").ToListAsync();
            return Page();
        }
    }
}
