using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.Panel.Pages.Warehouseincharge
{
    public class ViewIndentModel : PageModel
    {
        ApplicationDbContext _context;
        public string searching { get; set; }
        public ViewIndentModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGet(string jobworkid)
        {
            var search = new SqlParameter("@JobWorkId", jobworkid);
            SPMaterialIssuance = await _context.SPMaterialIssuance.FromSqlRaw("SPMaterialIssuance @JobWorkId", search).ToListAsync();
            searching = jobworkid;
            return Page();
        }

        public List<SPMaterialIssuance> SPMaterialIssuance { get; set; }
        public async Task<IActionResult> OnPostSearch(string searchtext)
        {
            var search = new SqlParameter("@JobWorkId", searchtext);
            SPMaterialIssuance = await _context.SPMaterialIssuance.FromSqlRaw("SPMaterialIssuance @JobWorkId", search).ToListAsync();
            searching = searchtext;
            return Page();
        }
    }
}
