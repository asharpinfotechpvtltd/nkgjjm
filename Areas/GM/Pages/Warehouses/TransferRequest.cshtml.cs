using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.GM.Pages.Warehouses
{
    public class TransferRequestModel : PageModel
    {
        ApplicationDbContext _context;
        public TransferRequestModel(ApplicationDbContext context)
        {
                _context = context;
        }
        public List<SpWareHouseTransferPendingListForHo> SpWareHouseTransferPendingListForHo { get; set; }
        public async Task<IActionResult> OnGet()
        {
            SpWareHouseTransferPendingListForHo = await _context.SpWareHouseTransferPendingListForHo.FromSqlRaw("SpWareHouseTransferPendingListForHo").ToListAsync();

            return Page();
        }
    }
}
