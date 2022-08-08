using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Classes;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.VillageIncharge.Pages.VillageIncharge
{
    public class WorkProgressModel : PageModel
    {
        ApplicationDbContext Context;
        IWebHostEnvironment Environmet;
        [BindProperty]
        public IFormFile challan { get; set; }
        public string challanName { get; set; }

        public WorkProgressModel(ApplicationDbContext context, IWebHostEnvironment Env)
        {
            Context = context;
            Environmet = Env;
        }
        [BindProperty]
        public string po { get; set; }
        public List<SelectListItem> JobWorklist { get; set; }
        public async Task<IActionResult> OnGet(string Search, string searchtext)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
            {
                int id = Convert.ToInt32(HttpContext.Session.GetString("Login"));
                if (string.IsNullOrEmpty(Search))
                {
                    var Warehouseid = await Context.TblVillageInchargeForWareHouse.FirstOrDefaultAsync(e => e.VillageInchargeId == id);
                    if (Warehouseid == null)
                    {
                        return Page();
                    }
                    else
                    {
                        int W_hid = Warehouseid.WarehouseId;
                        JobWorklist = await Context.TblJobWork.Select(j => new SelectListItem { Text = j.WorkorderId, Value = j.WorkorderId }).ToListAsync();
                        return Page();
                    }
                }
                return Page();
            }
            else
            {
                return Redirect("~/Index");
            }

        }
        public async Task<IActionResult> OnPost(string Jobworkid, List<IFormFile> siteimage)
        {
            try
            {
                GetUserDate date = new GetUserDate();
                Upload u = new Upload(Environmet);
                foreach (var formFile in siteimage)
                {
                    challanName = u.UploadImage(formFile, "SiteImage");
                    Closure c = new Closure()
                    {
                        Image = challanName,
                        Jobworkid = Jobworkid,
                        Date = date.ReturnDate(),
                        Submittedby = "Village Incharge"
                    };
                    await Context.TblClosure.AddAsync(c);
                    await Context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

            }

            return Page();
        }
    }
}