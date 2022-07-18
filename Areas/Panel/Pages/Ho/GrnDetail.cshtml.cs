using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.Ho
{
    public class GrnDetailModel : PageModel
    {
        ApplicationDbContext _context;

        public GrnDetailModel(ApplicationDbContext context)
        {
            _context = context;

        }
        [BindProperty]
        public PoMaster Pomaster { get; set; }
        [BindProperty]
        public InwardDocuments document { get; set; }

        
        public List<MaterialReceivedbyPo> materialReceivedbyPos { get; set; }
        public async Task<IActionResult> OnGet(string challanno)
        {
            document = await _context.TblInwardDocuments.SingleOrDefaultAsync(e => e.challanno == challanno);
            materialReceivedbyPos = await _context.TblMaterialReceivedbyPo.Where(e => e.Challan_Invoicenumber == challanno).ToListAsync();
            Pomaster = await _context.TblPoMaster.SingleOrDefaultAsync(c => c.Challan_Invoicenumber == challanno);


            
            return Page();
        }
    }
}
