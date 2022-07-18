using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nkgjjm.Classes;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.VillageIncharge
{
    public class ClosureModel : PageModel
    {
        ApplicationDbContext Context;
        IWebHostEnvironment Environmet;
        [BindProperty]
        public IFormFile challan { get; set; }
        public string challanName { get; set; }

        public ClosureModel(ApplicationDbContext context, IWebHostEnvironment Env)
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
        public async Task<IActionResult> OnPost(string PoNo)
        {
            Upload u = new Upload(Environmet);
            challanName = u.UploadImage(challan, "SiteImage");
            Challan c = new Challan()
            {
                ChallanName = challanName,
                Pono = PoNo
            };
            await Context.TblChallan.AddAsync(c);
            await Context.SaveChangesAsync();
            return Page();
        }
    }
}