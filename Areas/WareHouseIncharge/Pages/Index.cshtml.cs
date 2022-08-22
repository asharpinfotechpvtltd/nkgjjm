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
using Microsoft.Data.SqlClient;
using System.Web.WebPages;

namespace Nkgjjm.Areas.WareHouseIncharge.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public int Totalwarehouse { get; set; }
        public int TblCustomerOrderDetail { get; set; }
        public int TotalDistrict { get; set; }
        public int TotalBlock { get; set; }
        public int TotalGp { get; set; }
        public int TotalVillage { get; set; }
        public int TotalJobOrders { get; set; }
        public int TotalPo { get; set; }
        public int Totalindent { get; set; }     
        

        


        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<SPAssignedJobWorkToWareHouse> SPAssignedJobWorkToWareHouse { get; set; }
        public List<SpWareHouseStockReport> SpWareHouseStockReport { get; set; }
        public async Task<IActionResult> OnGet()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
            {
                var wh_id = await _context.TblWarehouseIncharge.FirstAsync(e => e.UserId == Convert.ToInt32(HttpContext.Session.GetString("Login")));
                int whid = wh_id.WareHouseid;
                var Warehouseid = new SqlParameter("@WareHouseId", whid);

                SPAssignedJobWorkToWareHouse = await _context.SPAssignedJobWorkToWareHouse.FromSqlRaw("SPAssignedJobWorkToWareHouse @WareHouseId", Warehouseid).ToListAsync();
                TotalPo = SPAssignedJobWorkToWareHouse.Count;
                TotalJobOrders = _context.TblJobWork.Where(e => e.Warehouseid == whid).Count();
                Totalindent = _context.TblIndentMaster.Where(e => e.WareHouseid == whid).Count();

                var Ware_house = new SqlParameter("@WareHouse", whid);
                SpWareHouseStockReport = await _context.SpWareHouseStockReport.FromSqlRaw("SpWareHouseStockReport @WareHouse", Ware_house).ToListAsync();


                return Page();
            }
            else
            {
                return Redirect("~/Index");
            }

        }
        //public async Task<IActionResult> OnGet()
        //{
        //    if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
        //    {
        //        Totalwarehouse = _context.TblWarehouse.Count();
        //        TotalDistrict = _context.TblDistrict.Count();
        //        TotalBlock = _context.TblBlock.Count();
        //        TotalGp = _context.TblGramPanchayat.Count();
        //        TotalVillage = _context.TblVillageCode.Count();
        //        TotalCount = await _context.SPTotalCount.FromSqlRaw("SPTotalCount").ToListAsync();
        //        TotalJobOrders = await _context.TblJobWork.CountAsync();
        //        TotalPo = await _context.TblPoMaster.CountAsync();
        //        Totalindent = await _context.TblIndentMaster.CountAsync();
        //        TotalActiveUser = await _context.TblUser.Where(e => e.Status == true).CountAsync();
        //        TotalActiveSite = await _context.TblJobWork.Where(e => e.Iscompleted == false).CountAsync();
        //        TotalUnderProcess = await _context.TblJobWork.Where(e => e.Iscompleted == false).CountAsync();
        //        TotalCompletedSite = await _context.TblJobWork.Where(e => e.Iscompleted == true).CountAsync();
        //        TotalUser = await _context.TblUser.CountAsync();
        //        TotalInactiveUser = await _context.TblUser.Where(u => u.Status == false).CountAsync();
        //        int Userid = Convert.ToInt32(HttpContext.Session.GetString("Login"));

        //        return Page();
        //    }
        //    else
        //    {
        //        return Redirect("~/Index");
        //    }



        //}


        public IActionResult OnGetLogout()
        {

            HttpContext.Session.Remove("Login");
            HttpContext.Session.Clear();
            return Redirect("./Index");

        }

    }
}
