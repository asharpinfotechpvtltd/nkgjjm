using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;
using Microsoft.AspNetCore.Http.Extensions;

namespace Nkgjjm.ViewComponents
{
    [ViewComponent(Name = "Menu")]
    public class CategoryViewComponent : ViewComponent
    {
        ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CategoryViewComponent(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        // public List<Menu> Menu { get; set; }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var UserEmail = new SqlParameter("@UserEmail", "s@gmail.com"); //HttpContext.Session.GetString("");

            List<SpUserMenu> Main = await _context.SpUserMenu.FromSqlRaw("SpUserMenu @UserEmail", UserEmail).ToListAsync();
            List<SpUserChildMenu> ChildMenu = await _context.SpUserChildMenu.FromSqlRaw("SpUserChildMenu @UserEmail", UserEmail).ToListAsync();
            string url = Request.Path;
            var isvalid = ChildMenu.SingleOrDefault(e => e.CONTROLLER_NAME == url);
            ChildViewModel models = new ChildViewModel(_context)
            {
                Menu = Main,
                ChildMenu = ChildMenu
            };

            return View("/Menu", models);

            //if (isvalid != null)
            //{
            //    ChildViewModel models = new ChildViewModel(_context)
            //    {
            //        Menu = Main,
            //        ChildMenu = ChildMenu
            //    };

            //    return View("/Menu", models);
            //}
            //else
            //{
            //    _httpContextAccessor.HttpContext.Response.Redirect("/AccessDenied");
            //    return View("/AccessDenied");
            //}
        }
    }
}
