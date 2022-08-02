using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public IActionResult OnGet(string PoNo)
        {
            po = PoNo;
            return Page();
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
            catch(Exception ex)
            {

            }

            return Page();
        }
    }
}