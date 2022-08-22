using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;


        public ApplicationDbContext Context;
        public IndexModel(ApplicationDbContext _Context)
        {
            Context = _Context;

        }
        public void OnGet()
        {

        }
        [BindProperty]
        public User Admin { get; set; }
        public async Task<IActionResult> OnPost()
        {
            User Admins = await Context.TblUser.FirstOrDefaultAsync(uname => uname.Email == Admin.Email && uname.Password == Admin.Password && uname.Status==true);
            if (Admins != null)
            {
                if (Admins.Designation == 1)
                {
                    HttpContext.Session.SetString("Login", Admins.Id.ToString());
                    return RedirectToPage("/Index", new { area = "Headoffice" });
                }
                else if(Admins.Designation==2)
                {
                    HttpContext.Session.SetString("Login", Admins.Id.ToString());
                    return RedirectToPage("/Index", new { area = "VillageIncharge" });
                }
                else if (Admins.Designation == 3)
                {
                    HttpContext.Session.SetString("Login", Admins.Id.ToString());
                    return RedirectToPage("/Index", new { area = "WareHouseIncharge" });
                }
                else if (Admins.Designation == 4)
                {
                    HttpContext.Session.SetString("Login", Admins.Id.ToString());
                    return RedirectToPage("/Index", new { area = "GM" });
                }
                else if (Admins.Designation == 5)
                {
                    HttpContext.Session.SetString("Login", Admins.Id.ToString());
                    return RedirectToPage("/Index", new { area = "Director" });
                }
                return Redirect("Index");
            }
            else
            {
                return Redirect("Index");
            }
           
        }
    }
}
