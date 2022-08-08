using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Nkgjjm.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckVillageInchargeController : ControllerBase
    {
        ApplicationDbContext _context;
        public CheckVillageInchargeController(ApplicationDbContext context)
        {
                _context = context; 
        }
      
        
        public async Task<string> Get(int WareHouseId,int VillageInchargeId)
        {
            var IsExist = await _context.TblVillageInchargeForWareHouse.FirstOrDefaultAsync(e => e.WarehouseId == WareHouseId && e.VillageInchargeId == VillageInchargeId);
            if (IsExist == null)
            {
                VillageIncharges v = new VillageIncharges
                {
                    VillageInchargeId = VillageInchargeId,
                    WarehouseId = WareHouseId
                };
                await _context.TblVillageInchargeForWareHouse.AddAsync(v);
                await _context.SaveChangesAsync();
                return "Valid";
            }
            else
            {
                return "AR";
            }
        }

        
    }
}
