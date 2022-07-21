using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> UpdateIndentStatus(string jobid, int IndentMasterid, Int64 Itemid)
        {
            var status =await _context.TblIndent.FirstOrDefaultAsync(e => e.Jobworkid == jobid && e.IndentMasterid == IndentMasterid && e.ItemCode == Itemid);
            if(status != null)
            {
                status.Status = "Approved";
            }
            await _context.SaveChangesAsync();
            return Ok("Success");
        }
    }
}
