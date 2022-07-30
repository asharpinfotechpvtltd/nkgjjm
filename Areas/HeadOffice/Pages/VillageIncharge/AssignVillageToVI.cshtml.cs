using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.Panel.Pages.VillageIncharge
{
    public class AssignVillageToVIModel : PageModel
    {
        ApplicationDbContext _context;
        public AssignVillageToVIModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }
        public List<SelectListItem> Districts { get; set; }
        [BindProperty]
       public VillageWithVillageIncharge incharge { get; set; }
        public List<SPAssignedVillageToVI> AssigedVillage { get; set; }
        public int id { get; set; }
        public async Task<IActionResult> OnGet(int Id)
        {
            id = Id;
            Districts = await _context.TblDistrict.Select(a => new SelectListItem { Text = a.District, Value = a.id.ToString() }).ToListAsync();
            var Inchargeid = new SqlParameter("@id", Id);
            HttpContext.Session.SetString("id", Convert.ToString(Id));
            AssigedVillage = await _context.SPAssignedVillageToVI.FromSqlRaw("SPAssignedVillageToVI @id", Inchargeid).ToListAsync();
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            await _context.TblVillageWithVillageIncharge.AddAsync(incharge);
            await _context.SaveChangesAsync();
            id = Convert.ToInt32(HttpContext.Session.GetString("id"));
            var Inchargeid = new SqlParameter("@id", id);
            AssigedVillage = await _context.SPAssignedVillageToVI.FromSqlRaw("SPAssignedVillageToVI @id", Inchargeid).ToListAsync();
            return Page();
        }
    }
}
