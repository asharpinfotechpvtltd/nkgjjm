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
                HttpContext.Session.SetString("Login", Admin.Email);
                return RedirectToPage("/Index", new { area = "panel" });
            }
            else
            {
                return Redirect("Index");
            }
        }
    }
}
