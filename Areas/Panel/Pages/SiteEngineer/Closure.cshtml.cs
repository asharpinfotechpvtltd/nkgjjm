using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nkgjjm.Classes;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.SiteEngineer
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
        public async Task<IActionResult> OnPost(string Jobworkid, List<IFormFile> siteimage)
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
                    Date=date.ReturnDate(),
                    Submittedby="Site Engineer"
                    
                };
                await Context.TblClosure.AddAsync(c);
                
            }
            var workid = Context.TblJobWork.SingleOrDefault(e => e.WorkorderId == Jobworkid);
            if(workid != null)
            {
                workid.Iscompleted = true;
            }
            await Context.SaveChangesAsync();
            return Page();
        }
    }
}