using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nkgjjm.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.WareHouseIncharge.Pages
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
        public int TotalJobOrders { get; set; }
        public int TotalPo { get; set; }
        public int Totalindent { get; set; }
        public int TotalActiveUser { get; set; }
        public int TotalActiveSite { get; set; }
        public int TotalUnderProcess { get; set; }
        public int TotalCompletedSite { get; set; }
        public int TotalUser{ get; set; }
        public int TotalInactiveUser { get; set; }

        public List<SPTotalCount> TotalCount { get; set; }
        

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
            TotalCount = await _context.SPTotalCount.FromSqlRaw("SPTotalCount").ToListAsync();
            TotalJobOrders = await _context.TblJobWork.CountAsync();
            TotalPo = await _context.TblPoMaster.CountAsync();
            Totalindent = await _context.TblIndentMaster.CountAsync();
            TotalActiveUser = await _context.TblUser.Where(e=>e.Status==true).CountAsync();
            TotalActiveSite = await _context.TblJobWork.Where(e=>e.Iscompleted==false).CountAsync();
            TotalUnderProcess = await _context.TblJobWork.Where(e=>e.Iscompleted==false).CountAsync();
            TotalCompletedSite = await _context.TblJobWork.Where(e=>e.Iscompleted==true).CountAsync();
            TotalUser = await _context.TblUser.CountAsync();
            TotalInactiveUser = await _context.TblUser.Where(u=>u.Status==false).CountAsync();
            int Userid = Convert.ToInt32(HttpContext.Session.GetString("Login"));
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
            HttpContext.Session.Clear();
            return Redirect("./Index");

        }

    }
}
