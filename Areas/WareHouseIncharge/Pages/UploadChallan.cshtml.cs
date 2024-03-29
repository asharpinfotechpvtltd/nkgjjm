using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Classes;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.WareHouseIncharge.Pages.Warehouseincharge
{
    public class UploadChallanModel : PageModel
    {
        ApplicationDbContext Context;
        IWebHostEnvironment Environmet;
        [BindProperty]
        public IFormFile challan { get; set; }
        public string challanName { get; set; }

        public UploadChallanModel(ApplicationDbContext context, IWebHostEnvironment Env)
        {
            Context = context;
            Environmet = Env;
        }
        [BindProperty]
        public string po { get; set; }
        public IActionResult OnGet(string PoNo)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
            {

                po = PoNo;
                return Page();
            }
            else
            {
                return Redirect("~/Index");
            }
        }
        public async Task<IActionResult> OnPost(string PoNo)
        {
            try
            {
                Upload u = new Upload(Environmet);
                challanName = u.UploadImage(challan, "Challan");
                Challan c = new Challan()
                {
                    ChallanName = challanName,
                    Pono = PoNo
                };
                await Context.TblChallan.AddAsync(c);
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
            return Page();
        }
    }
}
