using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Classes;
using Nkgjjm.Models;

namespace Nkgjjm.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndentController : ControllerBase
    {
        ApplicationDbContext _context;
        public IndentController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> UpdateIndentStatus(string jobid, int IndentMasterid, Int64 Itemid,int Warehouseid,int Qty)
        {
            GetUserDate date = new GetUserDate();
            var status =await _context.TblIndent.FirstOrDefaultAsync(e => e.Jobworkid == jobid && e.IndentMasterid == IndentMasterid && e.ItemCode == Itemid);
            if(status != null)
            {
                status.Status = "Approved";
            }
            StockPassbook passbook = new StockPassbook()
            {
                ItemCode = Itemid,
                Date = date.ReturnDate(),
                Warehouseid = Warehouseid,
                Freeze = Qty,
                IndentMaster= IndentMasterid
            };
            await _context.TblStockPassbook.AddAsync(passbook);
            await _context.SaveChangesAsync();
            return Ok("Success");
        }
    }
}
