using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockController : ControllerBase
    {
        ApplicationDbContext _context;
        public BlockController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]  
        public async Task<string> GetBlockbyDistrict(int Districtid, string BlockName)
        {
            var Dist = await _context.TblBlock.SingleOrDefaultAsync(Did => Did.District == Districtid && Did.Block == BlockName);
            if (Dist == null)
            {
                return "Ok";
            }
            else
            {
                return "NA";
            }

        }
    }
}
