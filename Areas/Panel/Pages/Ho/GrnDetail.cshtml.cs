using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
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
        public PoVehicleDetail PoVehicledetail { get; set; }
     

        
        public List<SPMaterialReceivedCorrespondenceToPo> SPMaterialReceivedCorrespondenceToPo { get; set; }
        public async Task<IActionResult> OnGet(string Pono, string challan)
        {
            var po = new SqlParameter("@PONo", Pono);
            var chln = new SqlParameter("@Challannumber", challan);
            SPMaterialReceivedCorrespondenceToPo = await _context.SPMaterialReceivedCorrespondenceToPo.FromSqlRaw("SPMaterialReceivedCorrespondenceToPo @PONo,@Challannumber",po,chln).ToListAsync();
            PoVehicledetail = await _context.TblPoVehicleDetail.SingleOrDefaultAsync(c => c.ChallanNumber == challan);


            
            return Page();
        }
    }
}
