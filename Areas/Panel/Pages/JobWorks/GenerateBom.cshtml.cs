using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Classes;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.Panel.Pages.JobWorks
{
    public class GenerateBomModel : PageModel
    {
        ApplicationDbContext _context;
        public GenerateBomModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Bom Bom { get; set; }
        public List<SPBomList> ListBom { get; set; }
        public List<SelectListItem> ItemMasters { get; set; }
        public List<ItemMaster> ItemList { get; set; }
        public string JobWorkid { get; set; }
        
        public async Task<IActionResult> OnGet(string id)
        {
            JobWorkid = id;
            ItemList = await _context.TblItemMaster.ToListAsync();
            // var Jobid = new SqlParameter("@JOBWORKID", id);
            ItemMasters = await _context.TblItemMaster.Select(a => new SelectListItem { Text = a.ItemName, Value = a.ItemId.ToString() }).ToListAsync();
            //ListBom = await _context.SPBomList.FromSqlRaw("SPBomList @JOBWORKID",Jobid).ToListAsync();

            return Page();
        }
        public async Task<IActionResult> OnPost(string JobWorkId)
        {
            Bom.JobWorkId = JobWorkId;
            GetUserDate date = new GetUserDate();
            Bom.AssignedDate = date.ReturnDate();
            await _context.TblBom.AddAsync(Bom);
            await _context.SaveChangesAsync();
            var Jobid = new SqlParameter("@JOBWORKID", JobWorkId);
            ListBom = await _context.SPBomList.FromSqlRaw("SPBomList @JOBWORKID", Jobid).ToListAsync();
            return Redirect("GenerateBom?id=" + JobWorkId);
        }
    }
}
