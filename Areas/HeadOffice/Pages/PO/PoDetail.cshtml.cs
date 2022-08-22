using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.HeadOffice.Pages.PO
{
    public class PoDetailModel : PageModel
    {
        ApplicationDbContext _context;
        public PoDetailModel(ApplicationDbContext context)
        {
            _context = context; 

        }
        public List<SPPoMasterDetail> SPPoMasterDetail { get; set; }
        public List<SPPoDetail> SPPoDetail { get; set; }
        public async Task<IActionResult> OnGet(string PoNo)
        {

            var Po = new SqlParameter("@Pono", PoNo);
            SPPoMasterDetail = await _context.SPPoMasterDetail.FromSqlRaw("SPPoMasterDetail @Pono", Po).ToListAsync();
            SPPoDetail = await _context.SPPoDetail.FromSqlRaw("SPPoDetail @Pono", Po).ToListAsync();

            return Page();
        }
    }
}
