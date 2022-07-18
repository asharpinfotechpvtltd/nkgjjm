using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.Ho
{
    public class GrnMasterModel : PageModel
    {
        ApplicationDbContext _context;
        public GrnMasterModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<PoMaster> Pomaster { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Pomaster = await  _context.TblPoMaster.ToListAsync();
            return Page();
        }
    }
}
