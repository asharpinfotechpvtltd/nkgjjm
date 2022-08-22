using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Classes;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.PO
{
    public class UploadGrnModel : PageModel
    {
        ApplicationDbContext Context;
        IWebHostEnvironment Environmet;
        [BindProperty]
        public IFormFile Uploadgrn { get; set; }
        public string UploadgrnName { get; set; }
        
        public UploadGrnModel(ApplicationDbContext context, IWebHostEnvironment Env)
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
        public IActionResult OnPost(string PoNo)
        {
            //Upload u = new Upload(Environmet);            
            //UploadgrnName = u.UploadImage(Uploadgrn, "Grn");
            //var grn = await Context.TblPoMaster.SingleOrDefaultAsync(e => e.Pono == PoNo);
            //if(grn != null)
            //{
            //    grn.Grn = UploadgrnName;
            //}
            //await Context.SaveChangesAsync();
            return Page();
        }
    }
}
