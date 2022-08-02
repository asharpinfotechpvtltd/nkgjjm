using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.Panel.Pages.Ho
{
    public class GrnMasterModel : PageModel
    {
        ApplicationDbContext _context;
        public GrnMasterModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<SpGrnMaster> Grmmaster { get; set; }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                Grmmaster = await _context.SpGrnMaster.FromSqlRaw("SpGrnMaster").ToListAsync();
            }
            catch(Exception ex)
            {

            }
            return Page();
        }
    }
}
