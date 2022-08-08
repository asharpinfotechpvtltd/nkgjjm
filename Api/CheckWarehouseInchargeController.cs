using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Nkgjjm.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckWarehouseInchargeController : ControllerBase
    {
        ApplicationDbContext _context;
        public CheckWarehouseInchargeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Get(int WareHouseId, int WarehouseInchargeId)
        {
            var iswarehouseinchargeexist = await _context.TblWarehouseIncharge.FirstOrDefaultAsync(e => e.UserId == WarehouseInchargeId);
            if (iswarehouseinchargeexist == null)
            {


                var IsExist = await _context.TblWarehouseIncharge.FirstOrDefaultAsync(e => e.WareHouseid == WareHouseId && e.UserId == WarehouseInchargeId);

                if (IsExist == null)
                {
                    WarehouseIncharges v = new WarehouseIncharges
                    {
                        UserId = WarehouseInchargeId,
                        WareHouseid = WareHouseId,
                    };
                    await _context.TblWarehouseIncharge.AddAsync(v);
                    await _context.SaveChangesAsync();
                    return "Valid";
                }

            }
            else
            {
                return "AR";
            }
            return "Valid";
        }


    }
}
