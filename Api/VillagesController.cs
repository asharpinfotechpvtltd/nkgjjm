using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public List<Villages> VillageList { get; set; }

        public VillagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Villages
       
        public async Task<ActionResult<IEnumerable<Villages>>> GetTblVillageCode(int id)
        {
            VillageList = await _context.TblVillageCode.Where(gp => gp.GramPanchayat == id).ToListAsync();
            return Ok(VillageList);
        }
        
    }
}
