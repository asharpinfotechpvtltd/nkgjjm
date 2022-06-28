using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nkgjjm.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Nkgjjm.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public int Totalwarehouse { get; set; }
        public int TblCustomerOrderDetail { get; set; }
        public double TotalSales { get; set; }

        public int TotalDriver { get; set; }
        [BindProperty]
        public int ProductSold { get; set; }
        

        public int TotalDistrict { get; set; }
        public int TotalBlock { get; set; }
        public int TotalGp { get; set; }
        public int TotalVillage { get; set; }
        

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGet()
        {
            Totalwarehouse = _context.TblWarehouse.Count();
            TotalDistrict = _context.TblDistrict.Count();
            TotalBlock = _context.TblBlock.Count();
            TotalGp = _context.TblGramPanchayat.Count();
            TotalVillage = _context.TblVillageCode.Count();
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
            //{
            //    DateTime timeUtc = System.DateTime.UtcNow;
            //    TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            //    DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
            //    string date = cstTime.ToString("dd-MM-yyyy");
            //    string time = cstTime.ToString("HH:mm");
            //    string logedin = HttpContext.Session.GetString("Login");

            //    
            //}
            //else
            //{
            //    return RedirectToPage("./AdminLogin");
            //}
            return Page();


        }


        public IActionResult OnGetLogout()
        {

            HttpContext.Session.Remove("Login");
            return Redirect("./Index");

        }

    }
}
