using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.WareHouseIncharge.Pages
{
    public class TransferInwardMasterModel : PageModel
    {
        ApplicationDbContext _context;
        public TransferInwardMasterModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {

            return Page();
        }
    }
}
